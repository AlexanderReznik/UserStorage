﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices.Notifications
{
    public interface INotificationSender
    {
        void Send(NotificationContainer container);

        void AddReceiver(INotificationReceiver receiver);
    }
}
