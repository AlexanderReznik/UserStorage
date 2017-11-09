using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices.Notifications
{
    public class NotificationSender : INotificationSender
    {
        public INotificationReceiver Receiver {get; set; }

        public NotificationSender(INotificationReceiver receiver = null)
        {
            Receiver = receiver ?? new NotificationReceiver();
        }

        public void Send(NotificationContainer container)
        {
            Receiver.Receive(container);
        }
    }
}
