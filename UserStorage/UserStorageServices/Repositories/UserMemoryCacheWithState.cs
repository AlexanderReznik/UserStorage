using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using UserStorageServices.Tests;
using System.Configuration;

namespace UserStorageServices.Repositories
{
    public class UserMemoryCacheWithState : UserMemoryCache
    {
        private string FileName { get; }

        public UserMemoryCacheWithState(string path = null)
        {
            if (string.IsNullOrEmpty(path))
            {
                path = "repository.bin";
            }
            FileName = path;
        }

        public override void Start()
        {
            var formatter = new BinaryFormatter();

            if (!File.Exists(FileName))
            {
                list = new List<User>();
            }
            else
            {
                using (FileStream fs = new FileStream(FileName, FileMode.Open))
                {
                    list = (List<User>) formatter.Deserialize(fs);
                }
            }
        }

        public override void Finish()
        {
            var formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, list);
            }
        }
    }
}
