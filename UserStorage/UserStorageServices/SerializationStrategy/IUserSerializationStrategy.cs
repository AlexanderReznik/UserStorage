using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices.SerializationStrategy
{
    public interface IUserSerializationStrategy
    {
        void SerializeUsers(List<User> list, int lastId, string fileName);

        List<User> DeserializeUsers(string fileName, out int lastId);
    }
}
