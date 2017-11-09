using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UserStorageServices.Notifications
{
    public class NotificationSender : INotificationSender
    {
        public INotificationReceiver Receiver {get; set; }

        private XmlSerializer Serializer { get; }

        public NotificationSender(INotificationReceiver receiver = null)
        {
            Receiver = receiver ?? new NotificationReceiver();
            Serializer = new XmlSerializer(typeof(NotificationContainer));
        }

        public void Send(NotificationContainer container)
        {
            string message;
            using (var stringWriter = new StringWriter())
            {
                Serializer.Serialize(stringWriter ,container);
                message = stringWriter.ToString();
            }
            
            Receiver.Receive(message);
        }
    }
}
