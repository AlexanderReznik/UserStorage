using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UserStorageServices.SerializationStrategy
{
    public class XmlUserSerializationStrategy : IUserSerializationStrategy
    {
        public void SerializeUsers(List<User> list, string fileName)
        {
            fileName = fileName + ".xml";
            var formatter = new XmlSerializer(typeof(List<User>));

            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                formatter.Serialize(fs, list);
            }
        }

        public List<User> DeserializeUsers(string fileName)
        {
            fileName = fileName + ".xml";
            var formatter = new XmlSerializer(typeof(List<User>));
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
