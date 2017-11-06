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
        public void SerializeUsers(List<User> list, int lastId, string fileName)
        {
            var listFileName = fileName + ".xml";
            var lastIdFileName = fileName + "Id.xml";
            var listFormatter = new XmlSerializer(typeof(List<User>));
            var intFormatter = new XmlSerializer(typeof(int));

            using (FileStream fs = new FileStream(listFileName, FileMode.Create))
            {
                listFormatter.Serialize(fs, list);
            }

            using (FileStream fs = new FileStream(lastIdFileName, FileMode.Create))
            {
                intFormatter.Serialize(fs, lastId);
            }
        }

        public List<User> DeserializeUsers(string fileName, out int lastId)
        {
            var listFileName = fileName + ".xml";
            var lastIdFileName = fileName + "Id.xml";
            var listFormatter = new XmlSerializer(typeof(List<User>));
            var intFormatter = new XmlSerializer(typeof(int));
            List<User> list;

            if (!File.Exists(listFileName))
            {
                list = new List<User>();
            }
            else
            {
                using (FileStream fs = new FileStream(listFileName, FileMode.Open))
                {
                    list = (List<User>)listFormatter.Deserialize(fs);
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
                    lastId = (int)intFormatter.Deserialize(fs);
                }
            }

            return list;
        }
    }
}
