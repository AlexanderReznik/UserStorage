using System;
using System.Diagnostics;
using UserStorageServices.Interfaces;

namespace UserStorageServices.Services
{
    public class UserStorageServiceSlave : UserStorageServiceBase, INotificationSubscriber
    {
        public UserStorageServiceSlave(IUserRepository repository = null) : base(repository)
        {
        }

        public override UserStorageServiceMode ServiceMode => UserStorageServiceMode.SlaveNode;

        /// <summary>
        /// Adds a new <see cref="User"/> to the storage.
        /// </summary>
        /// <param name="user">A new <see cref="User"/> that will be added to the storage.</param>
        public override void Add(User user)
        {
            StackTrace st = new StackTrace(true);
            if (this.CheckStackCall(st, "Void Add"))
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
        /// <param name="user">A user to remove</param>
        /// <returns>True if success</returns>
        public override bool Remove(User user)
        {
            StackTrace st = new StackTrace(true);
            if (this.CheckStackCall(st, "Boolean Remove"))
            {
                return base.Remove(user);
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

        public void UserRemoved(object sender, object user)
        {
            this.Remove((User)user);
        }

        private bool CheckStackCall(StackTrace st, string command)
        {
            int i = 1;
            for (; i < st.FrameCount; i++)
            {
                var frame = st.GetFrame(i);
                string className = frame.GetMethod().DeclaringType.FullName;
                string methodName = frame.GetMethod().ToString();
                if (className == typeof(UserStorageServiceMaster).FullName &&
                    methodName == $"{command}({typeof(User).FullName})")
                {
                    break;
                }
            }

            return i < st.FrameCount;
        }
    }
}
