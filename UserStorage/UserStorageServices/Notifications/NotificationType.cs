using System.Xml.Serialization;

namespace UserStorageServices.Notifications
{
    [XmlType(IncludeInSchema = false)]
    public enum NotificationType
    {
        [XmlEnum("addUser")]
        AddUser,

        [XmlEnum("deleteUser")]
        DeleteUser
    }
}
