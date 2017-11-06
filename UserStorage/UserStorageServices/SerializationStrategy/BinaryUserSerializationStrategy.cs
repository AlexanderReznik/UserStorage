using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace UserStorageServices.SerializationStrategy
{
    public class BinaryUserSerializationStrategy : IUserSerializationStrategy
    {
        public void SerializeUsers(List<User> list, int lastId, string fileName)
        {
            var listFileName = fileName + ".bin";
            var lastIdFileName = fileName + "Id.bin";
            var formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream(listFileName, FileMode.Create))
            {
                formatter.Serialize(fs, list);
            }

            using (FileStream fs = new FileStream(lastIdFileName, FileMode.Create))
            {
                formatter.Serialize(fs, lastId);
            }
        }

        public List<User> DeserializeUsers(string fileName, out int lastId)
        {
            var listFileName = fileName + ".bin";
            var lastIdFileName = fileName + "Id.bin";
            var formatter = new BinaryFormatter();
            List<User> list;

            if (!File.Exists(listFileName))
            {
                list = new List<User>();
            }
            else
            {
                using (FileStream fs = new FileStream(listFileName, FileMode.Open))
                {
                    list = (List<User>)formatter.Deserialize(fs);
                }
            }

            if (!File.Exists(lastIdFileName))
            {
                lastId = 1000;
            }
            else
            {
                using (FileStream fs = new FileStream(lastIdFileName, FileMode.Open))
                {
                    lastId = (int)formatter.Deserialize(fs);
                }
            }

            return list;
        }
    }
}
