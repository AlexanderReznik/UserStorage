using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UserStorageServices.Interfaces;
using UserStorageServices.Validators;

namespace UserStorageServices
{
    public enum UserStorageServiceMode
    {
        MasterNode,
        SlaveNode
    }

    /// <summary>
    /// Represents a service that stores a set of <see cref="User"/>s and allows to search through them.
    /// </summary>
    public class UserStorageService : IUserStorageService
    {
        /// <summary>
        /// Public c-tor to initialize storage.
        /// </summary>
        public UserStorageService(UserStorageServiceMode mode = UserStorageServiceMode.MasterNode, IEnumerable<IUserStorageService> slaves = null, IGeneratorId generatorId = null, IUserValidator userValidator = null)
        {
            Storage = new List<User>();
            GeneratorId = generatorId ?? new GeneratorGuid();
            UserValidator = userValidator ?? new DefaultUserValidator();
            Mode = mode;
            if (Mode == UserStorageServiceMode.MasterNode)
            {
                Slaves = slaves?.ToList() ?? new List<IUserStorageService>();
            }
            else
            {
                Slaves = null;
            }
        }

        /// <summary>
        /// Gets the number of elements contained in the storage.
        /// </summary>
        /// <returns>An amount of users in the storage.</returns>
        public int Count => Storage.Count;

        private List<User> Storage { get; }

        private IGeneratorId GeneratorId { get; }

        private IUserValidator UserValidator { get; }

        private UserStorageServiceMode Mode { get; }

        private List<IUserStorageService> Slaves { get; }

        /// <summary>
        /// Adds a new <see cref="User"/> to the storage.
        /// </summary>
        /// <param name="user">A new <see cref="User"/> that will be added to the storage.</param>
        public void Add(User user)
        {
            if (Mode == UserStorageServiceMode.MasterNode)
            {
                UserValidator.Validate(user);
                user.Id = GeneratorId.Generate();
                Storage.Add(user);
                foreach (var service in Slaves)
                {
                    service.Add(user);
                }
            }
            else
            {
                StackTrace st = new StackTrace(true);
                if (CheckStackCall(st, "Void Add"))
                {
                    Storage.Add(user);
                }
                else
                {
                    throw new NotSupportedException();
                }
            }
        }
        
        /// <summary>
        /// Removes an existed <see cref="User"/> from the storage.
        /// </summary>
        /// <param name="user">A user to remove</param>
        /// <returns>True if success</returns>
        public bool Remove(User user)
        {
            if (Mode == UserStorageServiceMode.MasterNode)
            {
                if (user == null)
                {
                    throw new ArgumentNullException($"{nameof(user)} is null.");
                }
                foreach (var service in Slaves)
                {
                    service.Remove(user);
                }
                return Storage.Remove(user);
            }
            else
            {
                StackTrace st = new StackTrace(true);
                if (CheckStackCall(st, "Boolean Remove"))
                {
                    return Storage.Remove(user);
                }
                else
                {
                    throw new NotSupportedException();
                }
            }
        }

        /// <summary>
        /// Searches through the storage for a <see cref="User"/> that matches specified criteria.
        /// </summary>
        /// <param name="predicate">Criteria for searching</param>
        /// <returns>User if exists, else null</returns>
        public User Search(Predicate<User> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException($"{nameof(predicate)} is null.");
            }

            return Storage.Find(predicate);
        }

        /// <summary>
        /// Searches through the storage for a <see cref="User"/> that matches specified criteria.
        /// </summary>
        /// <param name="firstName">First name for searching</param>
        /// <returns>User if exists, else null</returns>
        public User Search(string firstName)
        {
            if (firstName == null)
            {
                throw new ArgumentNullException($"{nameof(firstName)} is null.");
            }

            return Storage.Find(u => u.FirstName == firstName);
        }

        /// <summary>
        /// Searches through the storage for a <see cref="User"/> that matches specified criteria.
        /// </summary>
        /// <param name="predicate">Criteria for searching</param>
        /// <returns>IEnumerable with users</returns>
        public IEnumerable<User> SearchAll(Predicate<User> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException($"{nameof(predicate)} is null.");
            }

            return Storage.FindAll(predicate);
        }

        /// <summary>
        /// Searches through the storage for a <see cref="User"/> that matches specified criteria.
        /// </summary>
        /// <param name="firstName">First name for searching</param>
        /// <returns>IEnumerable with users</returns>
        public IEnumerable<User> SearchAll(string firstName)
        {
            if (firstName == null)
            {
                throw new ArgumentNullException($"{nameof(firstName)} is null.");
            }

            return Storage.FindAll(u => u.FirstName == firstName);
        }

        private bool CheckStackCall(StackTrace st, string command)
        {
            int i = 1;
            for (; i < st.FrameCount; i++)
            {
                var frame = st.GetFrame(i);
                string className = frame.GetMethod().DeclaringType.FullName;
                string methodName = frame.GetMethod().ToString();
                if (className == typeof(UserStorageService).FullName && methodName == $"{command}({typeof(User).FullName})")
                    break;
            }
            return i < st.FrameCount;
        }
    }
}
