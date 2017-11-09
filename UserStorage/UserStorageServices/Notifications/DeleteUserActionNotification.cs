using System.Xml.Serialization;

namespace UserStorageServices.Notifications
{
    public class DeleteUserActionNotification
    {
        [XmlElement("userId")]
        public int UserId { get; set; }
    }
}
