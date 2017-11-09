using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UserStorageServices.Notifications
{
    public class NotificationReceiver : INotificationReceiver
    {
        public event Action<NotificationContainer> Received;

        private XmlSerializer Serializer { get; }

        public NotificationReceiver()
        {
            Received = container => { };
            Serializer = new XmlSerializer(typeof(NotificationContainer));
        }

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
