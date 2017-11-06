using System;
using System.Collections.Generic;
using UserStorageServices.IdGenerators;
using UserStorageServices.Interfaces;

namespace UserStorageServices.Tests
{
    public class DefaultUserRepository : IUserRepository
    {
        protected IGeneratorId GeneratorId { get; }
        
        protected List<User> list;

        public DefaultUserRepository(IGeneratorId generatorId = null)
        {
            list = new List<User>();
            GeneratorId = generatorId ?? new GeneratorInt();
        }

        public virtual void Start() { }
        
        public virtual void Finish() { }

        public User Get(int id)
        {
            return list.Find(u => u.Id == id);
        }

        public void Set(User user)
        {
            if (user.Id == null)
            {
                user.Id = GeneratorId.Generate();
            }
            list.Add(user);
        }

        public bool Delete(User user)
        {
            return list.Remove(user);
        }

        public int Count => list.Count;

        public IEnumerable<User> Query(Predicate<User> predicate)
        {
            return list.FindAll(predicate);
        }
    }
}