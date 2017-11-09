using System;
using System.Collections.Generic;
using System.Linq;
using UserStorageServices.Interfaces;
using UserStorageServices.Notifications;
using UserStorageServices.Repositories;
using UserStorageServices.Validators;

namespace UserStorageServices.Services
{
    public class UserStorageServiceMaster : UserStorageServiceBase
    {
        public UserStorageServiceMaster(IUserValidator userValidator = null, IEnumerable<IUserStorageService> slaves = null, IUserRepository repository = null, INotificationSender sender = null) : base(repository)
        {
            this.UserValidator = userValidator ?? new DefaultUserValidator();
            this.Slaves = slaves?.ToList() ?? new List<IUserStorageService>();
            this.UserAdded = (a, b) => { };
            this.UserRemoved = (a, b) => { };
            this.Sender = sender ?? new CompositeNotificationSender();
        }

        private event EventHandler<User> UserAdded;

        private event EventHandler<int> UserRemoved;

        public INotificationSender Sender { get; }

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

            this.Sender.Send(new NotificationContainer()
            {
                Notifications = new[] 
                {
                    new Notification()
                    {
                        Type = NotificationType.AddUser,
                        Action = new AddUserActionNotification()
                        {
                            User = user
                        }
                    }
                }
            });
        }

        /// <summary>
        /// Removes an existed <see cref="User"/> from the storage.
        /// </summary>
        /// <param name="id">Id of user to remove</param>
        /// <returns>True if success</returns>
        public override bool Remove(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException($"{nameof(id)} is null.");
            }

            int newId = (int)id;
            this.UserRemoved(this, newId);

            foreach (var slave in this.Slaves)
            {
                slave.Remove(newId);
            }

            this.Sender.Send(new NotificationContainer()
            {
                Notifications = new[]
                {
                    new Notification()
                    {
                        Type = NotificationType.DeleteUser,
                        Action = new DeleteUserActionNotification()
                        {
                            UserId = newId
                        }
                    }
                }
            });

            return base.Remove(newId);
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
