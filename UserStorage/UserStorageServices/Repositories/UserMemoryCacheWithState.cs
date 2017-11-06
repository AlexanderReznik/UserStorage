using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using UserStorageServices.Tests;

namespace UserStorageServices.Repositories
{
    public class UserMemoryCacheWithState : UserMemoryCache
    {
        public override void Start()
        {
            var formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream("repository.bin", FileMode.Open))
            {
                list = (List<User>)formatter.Deserialize(fs);
            }
        }

        public override void Finish()
        {
            var formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream("repository.bin", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, list);
            }
        }
    }
}
