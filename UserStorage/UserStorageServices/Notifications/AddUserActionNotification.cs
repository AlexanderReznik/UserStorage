﻿using System.Xml.Serialization;

namespace UserStorageServices.Notifications
{
    public class AddUserActionNotification
    {
        [XmlElement("user")]
        public User User { get; set; }
    }
}
