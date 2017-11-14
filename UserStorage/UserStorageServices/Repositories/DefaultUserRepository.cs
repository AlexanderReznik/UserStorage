using System;
using System.Collections.Generic;
using UserStorageServices.IdGenerators;

namespace UserStorageServices.Repositories
{
    public class DefaultUserRepository : MarshalByRefObject, IUserRepository
    {
        public DefaultUserRepository(IGeneratorId generatorId = null)
        {
            this.List = new List<User>();
            this.GeneratorId = generatorId ?? new GeneratorInt();
        }

        public int Count => this.List.Count;

        protected List<User> List { get; set; }

        protected IGeneratorId GeneratorId { get; }

        public virtual void Start()
        {
        }

        public virtual void Stop()
        {
        }

        public User Get(int id)
        {
            return this.List.Find(u => u.Id == id);
        }

        public void Set(User user)
        {
            if (user.Id == null)
            {
                user.Id = this.GeneratorId.Generate();
            }

            this.List.Add(user);
        }

        public bool Delete(int id)
        {
            var listUser = this.List.Find(user => user.Id == id);
            if (listUser == null)
            {
                return false;
            }

            return this.List.Remove(listUser);
        }

        public IEnumerable<User> Query(Predicate<User> predicate)
        {
            return this.List.FindAll(predicate);
        }
    }
}