using System;
using System.Collections.Generic;

namespace UserStorageServices
{
    /// <summary>
    /// Represents a service that stores a set of <see cref="User"/>s and allows to search through them.
    /// </summary>
    public class UserStorageService
    {
        /// <summary>
        /// Public c-tor to initialize storage.
        /// </summary>
        public UserStorageService(IGeneratorId generatorId = null)
        {
            Storage = new List<User>();
            GeneratorId = generatorId ?? new GeneratorGuid();
        }

        /// <summary>
        /// Gets the number of elements contained in the storage.
        /// </summary>
        /// <returns>An amount of users in the storage.</returns>
        public int Count => Storage.Count;

        /// <summary>
        /// Enables logging
        /// </summary>
        public bool IsLoggingEnabled { get; set; }

        private List<User> Storage { get; }

        private IGeneratorId GeneratorId { get; }

        /// <summary>
        /// Adds a new <see cref="User"/> to the storage.
        /// </summary>
        /// <param name="user">A new <see cref="User"/> that will be added to the storage.</param>
        public void Add(User user)
        {
            if (IsLoggingEnabled)
            {
                Console.WriteLine("Add() method is called.");
            }

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrWhiteSpace(user.FirstName))
            {
                throw new ArgumentException("FirstName is null or empty or whitespace", nameof(user));
            }

            if (string.IsNullOrWhiteSpace(user.LastName))
            {
                throw new ArgumentException("LastName is null or empty or whitespace", nameof(user));
            }

            if (user.Age < 0)
            {
                throw new ArgumentException("Age is incorrect", nameof(user));
            }

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
            if (IsLoggingEnabled)
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
            if (IsLoggingEnabled)
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
            if (IsLoggingEnabled)
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
            if (IsLoggingEnabled)
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
            if (IsLoggingEnabled)
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
