using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using UserStorageServices.Tests;
using System.Configuration;
using UserStorageServices.Interfaces;
using UserStorageServices.SerializationStrategy;

namespace UserStorageServices.Repositories
{
    public class UserRepositoryWithState : DefaultUserRepository
    {
        private IUserSerializationStrategy Serializer { get; }

        private string FileName { get; }

        public UserRepositoryWithState(string path = null, IUserSerializationStrategy serializer = null)
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
            int lastId;
            list = Serializer.DeserializeUsers(FileName, out lastId);
            GeneratorId.LastId = lastId;
            Console.WriteLine(GeneratorId.LastId);
        }

        public override void Finish()
        {
            Serializer.SerializeUsers(list, GeneratorId.LastId, FileName);
        }
    }
}
