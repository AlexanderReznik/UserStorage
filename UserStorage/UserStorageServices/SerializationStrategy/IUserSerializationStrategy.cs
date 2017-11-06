using System.Collections.Generic;

namespace UserStorageServices.SerializationStrategy
{
    public interface IUserSerializationStrategy
    {
        void SerializeUsers(List<User> list, int lastId, string fileName);

        List<User> DeserializeUsers(string fileName, out int lastId);
    }
}
