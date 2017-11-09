using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UserStorageServices.Notifications
{
    class CompositeNotificationSender : INotificationSender
    {
        private List<INotificationSender> Senders;

        public CompositeNotificationSender()
        {
            Senders = new List<INotificationSender>();
        }

        public void Send(NotificationContainer container)
        {
            foreach (var sender in Senders)
            {
                sender.Send(container);
            }
        }

        public void AddReceiver(INotificationReceiver receiver)
        {
            if (receiver == null)
            {
                throw new ArgumentNullException();
            }

            var sender = new NotificationSender();
            sender.AddReceiver(receiver);

            Senders.Add(sender);
        }
    }
}
