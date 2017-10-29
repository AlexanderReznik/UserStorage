using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace UserStorageServices
{
    /// <summary>
    /// Represents a service that stores a set of <see cref="User"/>s and allows to search through them.
    /// </summary>
    public class UserStorageService : IUserStorageService
    {
        /// <summary>
        /// Enables logging
        /// </summary>
        private readonly BooleanSwitch _loggingSwitch = new BooleanSwitch("enableLogging", "Switch for logging");

        /// <summary>
        /// Public c-tor to initialize storage.
        /// </summary>
        public UserStorageService(IGeneratorId generatorId = null, IUserValidator userValidator = null)
        {
            Storage = new List<User>();
            GeneratorId = generatorId ?? new GeneratorGuid();
            UserValidator = userValidator ?? new DefaultUserValidator();
        }

        /// <summary>
        /// Gets the number of elements contained in the storage.
        /// </summary>
        /// <returns>An amount of users in the storage.</returns>
        public int Count => Storage.Count;

        private List<User> Storage { get; }

        private IGeneratorId GeneratorId { get; }

        private IUserValidator UserValidator { get; }

        /// <summary>
        /// Adds a new <see cref="User"/> to the storage.
        /// </summary>
        /// <param name="user">A new <see cref="User"/> that will be added to the storage.</param>
        public void Add(User user)
        {
            if (_loggingSwitch.Enabled)
            {
                Console.WriteLine("Add() method is called.");
            }

            UserValidator.Validate(user);
            user.Id = GeneratorId.Generate();
            Storage.Add(user);
        }

        /// <summary>
        /// Removes an existed <see cref="User"/> from the storage.
        /// </summary>
        /// <param name="user">A user to remove</param>
        /// <returns>True if success</returns>
        public bool Remove(User user)
        {
            if (_loggingSwitch.Enabled)
            {
                Console.WriteLine("Remove() method is called.");
            }

            if (user == null)
            {
                throw new ArgumentNullException($"{nameof(user)} is null.");
            }
        
            return Storage.Remove(user);
        }

        /// <summary>
        /// Searches through the storage for a <see cref="User"/> that matches specified criteria.
        /// </summary>
        /// <param name="predicate">Criteria for searching</param>
        /// <returns>User if exists, else null</returns>
        public User Search(Predicate<User> predicate)
        {
            if (_loggingSwitch.Enabled)
            {
                Console.WriteLine("Search() method is called.");
            }

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
            if (_loggingSwitch.Enabled)
            {
                Console.WriteLine("Search() method is called.");
            }

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
            if (_loggingSwitch.Enabled)
            {
                Console.WriteLine("SearchAll() method is called.");
            }

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
            if (_loggingSwitch.Enabled)
            {
                Console.WriteLine("SearchAll() method is called.");
            }

            if (firstName == null)
            {
                throw new ArgumentNullException($"{nameof(firstName)} is null.");
            }

            return Storage.FindAll(u => u.FirstName == firstName);
        }
    }
}
