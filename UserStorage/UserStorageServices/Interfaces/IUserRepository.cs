using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices.Interfaces
{
    public interface IUserRepository
    {
        void Start();

        void Finish();

        User Get(int id);

        void Set(User user);

        bool Delete(User user);

        int Count { get; }

        IEnumerable<User> Query(Predicate<User> predicate);
    }
}
