using System;
using System.IO;
using System.Xml.Serialization;

namespace UserStorageServices.Notifications
{
    public class NotificationSender : MarshalByRefObject, INotificationSender
    {
        public NotificationSender(INotificationReceiver receiver = null)
        {
            this.Receiver = receiver ?? new NotificationReceiver();
            this.Serializer = new XmlSerializer(typeof(NotificationContainer));
        }

        private INotificationReceiver Receiver { get; set; }

        private XmlSerializer Serializer { get; }

        public void AddReceiver(INotificationReceiver receiver)
        {
            if (receiver == null)
            {
                throw new ArgumentNullException($"{nameof(receiver)} is null.");
            }

            this.Receiver = receiver;
        }

        public void Send(NotificationContainer container)
        {
            string message;
            using (var stringWriter = new StringWriter())
            {
                this.Serializer.Serialize(stringWriter, container);
                message = stringWriter.ToString();
            }

            this.Receiver.Receive(message);
        }
    }
}
