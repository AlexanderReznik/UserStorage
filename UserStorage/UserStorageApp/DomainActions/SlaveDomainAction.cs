using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorageServices.Logging;
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
            Receiver = slave.Receiver;
            Slave = slave;
        }
    }
}
