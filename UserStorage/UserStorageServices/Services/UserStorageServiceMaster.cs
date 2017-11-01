using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorageServices.Interfaces;
using UserStorageServices.Validators;

namespace UserStorageServices.Services
{
    public class UserStorageServiceMaster : UserStorageServiceBase
    {
        public UserStorageServiceMaster(IGeneratorId generatorId = null, IUserValidator userValidator = null, IEnumerable<IUserStorageService> slaves = null) : base()
        {
            GeneratorId = generatorId ?? new GeneratorGuid();
            UserValidator = userValidator ?? new DefaultUserValidator();
            Subscribers = new List<INotificationSubscriber>();
            Slaves = slaves?.ToList() ?? new List<IUserStorageService>();
        }

        private List<INotificationSubscriber> Subscribers { get; }

        private List<IUserStorageService> Slaves { get; }

        private IGeneratorId GeneratorId { get; }

        private IUserValidator UserValidator { get; }

        public override UserStorageServiceMode ServiceMode => UserStorageServiceMode.MasterNode;

        /// <summary>
        /// Adds a new <see cref="User"/> to the storage.
        /// </summary>
        /// <param name="user">A new <see cref="User"/> that will be added to the storage.</param>
        public override void Add(User user)
        {
            UserValidator.Validate(user);
            user.Id = GeneratorId.Generate();
            base.Add(user);
            foreach (var subscriber in Subscribers)
            {
                subscriber.UserAdded(user);
            }

            foreach (var slave in Slaves)
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
            foreach (var subscriber in Subscribers)
            {
                subscriber.UserRemoved(user);
            }

            foreach (var slave in Slaves)
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
            Subscribers.Add(subscriber);
        }

        public void RemoveSubscriber(INotificationSubscriber subscriber)
        {
            if (subscriber == null)
            {
                throw new ArgumentNullException($"{nameof(subscriber)} is null.");
            }
            Subscribers.Remove(subscriber);
        }
    }
}
