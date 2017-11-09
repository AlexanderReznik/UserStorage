using System;
using System.Collections.Generic;

namespace UserStorageServices.Notifications
{
    public class CompositeNotificationSender : INotificationSender
    {
        public CompositeNotificationSender()
        {
            Senders = new List<INotificationSender>();
        }

        private List<INotificationSender> Senders { get; }

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
