using System;
using System.Collections.Generic;
using System.Linq;
using UserStorageServices.Repositories;

namespace UserStorageServices.Services
{
    public enum UserStorageServiceMode
    {
        MasterNode,
        SlaveNode
    }

    /// <summary>
    /// Represents a service that stores a set of <see cref="User"/>s and allows to search through them.
    /// </summary>
    public abstract class UserStorageServiceBase : MarshalByRefObject, IUserStorageService
    {
        /// <summary>
        /// Public c-tor to initialize storage.
        /// </summary>
        protected UserStorageServiceBase(IUserRepository repository = null)
        {
            this.Repository = repository ?? new DefaultUserRepository();
        }

        /// <summary>
        /// Gets the number of elements contained in the storage.
        /// </summary>
        /// <returns>An amount of users in the storage.</returns>
        public int Count => this.Repository.Count;

        public abstract UserStorageServiceMode ServiceMode { get; }

        private IUserRepository Repository { get; }

        /// <summary>
        /// Adds a new <see cref="User"/> to the storage.
        /// </summary>
        /// <param name="user">A new <see cref="User"/> that will be added to the storage.</param>
        public virtual void Add(User user)
        {
            this.Repository.Set(user);
        }

        /// <summary>
        /// Removes an existed <see cref="User"/> from the storage.
        /// </summary>
        /// <param name="id">Id of user to remove</param>
        /// <returns>True if success</returns>
        public virtual bool Remove(int? id)
        {
            return this.Repository.Delete((int)id);
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

            return this.Repository.Query(predicate).FirstOrDefault();
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

            return this.Repository.Query(u => u.FirstName == firstName).FirstOrDefault();
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

            return this.Repository.Query(predicate);
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

            return this.Repository.Query(u => u.FirstName == firstName);
        }
    }
}
