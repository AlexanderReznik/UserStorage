using System;
using System.Collections.Generic;

namespace UserStorageServices.Interfaces
{
    public interface IUserRepository
    {
        int Count { get; }

        void Start();

        void Finish();

        User Get(int id);

        void Set(User user);

        bool Delete(User user);

        IEnumerable<User> Query(Predicate<User> predicate);
    }
}
