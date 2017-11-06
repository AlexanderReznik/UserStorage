using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices.SerializationStrategy
{
    public class BinaryUserSerializationStrategy : IUserSerializationStrategy
    {
        public void SerializeUsers(List<User> list, string fileName)
        {
            fileName = fileName + ".bin";
            var formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                formatter.Serialize(fs, list);
            }
        }

        public List<User> DeserializeUsers(string fileName)
        {
            fileName = fileName + ".bin";
            var formatter = new BinaryFormatter();
            List<User> list;

            if (!File.Exists(fileName))
            {
                list = new List<User>();
            }
            else
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Open))
                {
                    list = (List<User>)formatter.Deserialize(fs);
                }
            }

            return list;
        }
    }
}
