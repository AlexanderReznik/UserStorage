using System;
using System.Collections.Generic;
using System.Linq;
using UserStorageServices.IdGenerators;
using UserStorageServices.Interfaces;
using UserStorageServices.Validators;

namespace UserStorageServices.Services
{
    public class UserStorageServiceMaster : UserStorageServiceBase
    {
        public UserStorageServiceMaster(IUserValidator userValidator = null, IEnumerable<IUserStorageService> slaves = null, IUserRepository repository = null) : base(repository)
        {
            this.UserValidator = userValidator ?? new DefaultUserValidator();
            this.Slaves = slaves?.ToList() ?? new List<IUserStorageService>();
            this.UserAdded = (a, b) => { };
            this.UserRemoved = (a, b) => { };
        }

        private event EventHandler<User> UserAdded;

        private event EventHandler<User> UserRemoved;

        public override UserStorageServiceMode ServiceMode => UserStorageServiceMode.MasterNode;

        private List<IUserStorageService> Slaves { get; }

        private IUserValidator UserValidator { get; }

        /// <summary>
        /// Adds a new <see cref="User"/> to the storage.
        /// </summary>
        /// <param name="user">A new <see cref="User"/> that will be added to the storage.</param>
        public override void Add(User user)
        {
            this.UserValidator.Validate(user);
            base.Add(user);
            this.UserAdded(this, user);

            foreach (var slave in this.Slaves)
            {
                slave.Add(user);
            }
        }

        /// <summary>
        /// Removes an existed <see cref="User"/> from the storage.
        /// </summary>
        /// <param name="user">A user to remove</param>
        /// <returns>True if success</returns>
        public override bool Remove(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException($"{nameof(user)} is null.");
            }

            this.UserRemoved(this, user);

            foreach (var slave in this.Slaves)
            {
                slave.Remove(user);
            }

            return base.Remove(user);
        }

        public void AddSubscriber(INotificationSubscriber subscriber)
        {
            if (subscriber == null)
            {
                throw new ArgumentNullException($"{nameof(subscriber)} is null.");
            }

            this.UserAdded += subscriber.UserAdded;
            this.UserRemoved += subscriber.UserRemoved;
        }

        public void RemoveSubscriber(INotificationSubscriber subscriber)
        {
            if (subscriber == null)
            {
                throw new ArgumentNullException($"{nameof(subscriber)} is null.");
            }

            this.UserAdded -= subscriber.UserAdded;
            this.UserRemoved -= subscriber.UserRemoved;
        }
    }
}
