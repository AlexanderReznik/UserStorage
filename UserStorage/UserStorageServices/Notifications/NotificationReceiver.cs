using System;
using System.IO;
using System.Xml.Serialization;

namespace UserStorageServices.Notifications
{
    public class NotificationReceiver : MarshalByRefObject, INotificationReceiver
    {
        public NotificationReceiver()
        {
            this.Received = container => { };
            this.Serializer = new XmlSerializer(typeof(NotificationContainer));
        }

        public event Action<NotificationContainer> Received;

        private XmlSerializer Serializer { get; }

        public void Receive(string message)
        {
            NotificationContainer container;
            using (var stringReader = new StringReader(message))
            {
                container = (NotificationContainer)this.Serializer.Deserialize(stringReader);
            }

            this.Received(container);
        }
    }
}
