using System;
using System.IO;
using System.Xml.Serialization;

namespace UserStorageServices.Notifications
{
    public class NotificationReceiver : INotificationReceiver
    {
        public NotificationReceiver()
        {
            Received = container => { };
            Serializer = new XmlSerializer(typeof(NotificationContainer));
        }

        public event Action<NotificationContainer> Received;

        private XmlSerializer Serializer { get; }

        public void Receive(string message)
        {
            NotificationContainer container;
            using (var stringReader = new StringReader(message))
            {
                container = (NotificationContainer)Serializer.Deserialize(stringReader);
            }

            Received(container);
        }
    }
}
