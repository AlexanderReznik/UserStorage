using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices.Interfaces
{
    public interface INotificationSubscriber
    {
        void UserAdded(object sender, object user);

        void UserRemoved(object sender, object user);
    }
}
