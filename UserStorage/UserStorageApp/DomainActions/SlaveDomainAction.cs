using System;
using UserStorageServices.Notifications;
using UserStorageServices.Services;

namespace UserStorageApp.DomainActions
{
    public class SlaveDomainAction : MarshalByRefObject
    {
        public INotificationReceiver Receiver { get; private set; }

        public IUserStorageService Slave { get; private set; }

        public void Run()
        {
            var slave = new UserStorageServiceSlave();
            this.Receiver = slave.Receiver;
            this.Slave = slave;
        }
    }
}
