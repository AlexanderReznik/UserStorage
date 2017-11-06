using System;
using UserStorageServices.SerializationStrategy;

namespace UserStorageServices.Repositories
{
    public class UserRepositoryWithState : DefaultUserRepository
    {
        public UserRepositoryWithState(string path = null, IUserSerializationStrategy serializer = null)
        {
            if (string.IsNullOrEmpty(path))
            {
                path = "repository";
            }

            this.FileName = path;
            this.Serializer = serializer ?? new BinaryUserSerializationStrategy();
        }

        private IUserSerializationStrategy Serializer { get; }

        private string FileName { get; }

        public override void Start()
        {
            int lastId;
            this.List = this.Serializer.DeserializeUsers(this.FileName, out lastId);
            GeneratorId.LastId = lastId;
            Console.WriteLine(GeneratorId.LastId);
        }

        public override void Finish()
        {
            this.Serializer.SerializeUsers(this.List, this.GeneratorId.LastId, this.FileName);
        }
    }
}
