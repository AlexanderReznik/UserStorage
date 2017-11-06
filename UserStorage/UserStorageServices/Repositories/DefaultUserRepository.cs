using System;
using System.Collections.Generic;
using UserStorageServices.Interfaces;

namespace UserStorageServices.Tests
{
    public class DefaultUserRepository : IUserRepository
    {
        protected List<User> list;

        public DefaultUserRepository()
        {
            list = new List<User>();
        }

        public virtual void Start() { }
        
        public virtual void Finish() { }

        public User Get(Guid id)
        {
            return list.Find(u => u.Id == id);
        }

        public void Set(User user)
        {
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