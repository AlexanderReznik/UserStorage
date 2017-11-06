using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using UserStorageServices.Tests;
using System.Configuration;
using UserStorageServices.SerializationStrategy;

namespace UserStorageServices.Repositories
{
    public class UserMemoryCacheWithState : UserMemoryCache
    {
        private IUserSerializationStrategy Serializer { get; }

        private string FileName { get; }

        public UserMemoryCacheWithState(string path = null, IUserSerializationStrategy serializer = null)
        {
            if (string.IsNullOrEmpty(path))
            {
                path = "repository";
            }
            FileName = path;
            Serializer = serializer ?? new BinaryUserSerializationStrategy();
        }

        public override void Start()
        {
            list = Serializer.DeserializeUsers(FileName);
        }

        public override void Finish()
        {
            Serializer.SerializeUsers(list, FileName);
        }
    }
}
