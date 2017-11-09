using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices.Notifications
{
    public class NotificationReceiver : INotificationReceiver
    {
        public event Action<NotificationContainer> Received;

        public NotificationReceiver()
        {
            Received = container => { };
        }

        public void Receive(NotificationContainer container)
        {
            Received(container);
        }
    }
}
