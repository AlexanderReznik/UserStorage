using System;
using System.Diagnostics;
using UserStorageServices.Interfaces;
using UserStorageServices.Notifications;
using UserStorageServices.Repositories;

namespace UserStorageServices.Services
{
    public class UserStorageServiceSlave : UserStorageServiceBase, INotificationSubscriber
    {
        public UserStorageServiceSlave(IUserRepository repository = null) : base(repository)
        {
            var reseiver = new NotificationReceiver();
            reseiver.Received += this.NotificationReceived;
            this.Receiver = reseiver;
        }

        public INotificationReceiver Receiver { get; }

        public override UserStorageServiceMode ServiceMode => UserStorageServiceMode.SlaveNode;

        /// <summary>
        /// Adds a new <see cref="User"/> to the storage.
        /// </summary>
        /// <param name="user">A new <see cref="User"/> that will be added to the storage.</param>
        public override void Add(User user)
        {
            StackTrace st = new StackTrace(true);
            if (this.CheckStackCall(st, "Void Add", typeof(User).FullName))
            {
                base.Add(user);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Removes an existed <see cref="User"/> from the storage.
        /// </summary>
        /// <param name="id">Id of user to remove</param>
        /// <returns>True if success</returns>
        public override bool Remove(int? id)
        {
            StackTrace st = new StackTrace(true);
            if (this.CheckStackCall(st, "Boolean Remove", $"System.{typeof(int?).Name}[{typeof(int).FullName}]"))
            {
                return base.Remove(id);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public void UserAdded(object sender, object user)
        {
            this.Add((User)user);
        }

        public void UserRemoved(object sender, int id)
        {
            this.Remove(id);
        }

        private void NotificationReceived(NotificationContainer container)
        {
            foreach (var notification in container.Notifications)
            {
                if (notification.Type == NotificationType.AddUser)
                {
                    var user = ((AddUserActionNotification)notification.Action).User;
                    this.Add(user);
                }
                else if (notification.Type == NotificationType.DeleteUser)
                {
                    var id = ((DeleteUserActionNotification)notification.Action).UserId;
                    this.Remove(id);
                }
            }
        }

        private bool CheckStackCall(StackTrace st, string command, string argumentType)
        {
            int i = 1;
            for (; i < st.FrameCount; i++)
            {
                var frame = st.GetFrame(i);
                string className = frame.GetMethod().DeclaringType.FullName;
                string methodName = frame.GetMethod().ToString();
                if (className == typeof(UserStorageServiceMaster).FullName &&
                    methodName == $"{command}({argumentType})")
                {
                    break;
                }
            }

            return i < st.FrameCount;
        }
    }
}
