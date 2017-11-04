using System;
using System.Collections.Generic;
using UserStorageServices.Interfaces;

namespace UserStorageServices.Tests
{
    public class UserMemoryCache : IUserRepository
    {
        private List<User> list;

        public UserMemoryCache()
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
            var repUser = Get(user.Id);
            if (repUser == null)
            {
                list.Add(user);
            }
            else
            {
                repUser.LastName = user.LastName;
                repUser.FirstName = user.FirstName;
                repUser.Age = user.Age;
            }
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