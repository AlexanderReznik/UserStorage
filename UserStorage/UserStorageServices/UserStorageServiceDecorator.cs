using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorageServices.Interfaces;
using UserStorageServices.Services;

namespace UserStorageServices
{
    public abstract class UserStorageServiceDecorator : IUserStorageService
    {
        protected UserStorageServiceDecorator(IUserStorageService service)
        {
            UserStorageService = service ?? new UserStorageServiceSlave();
        }

        public int Count => UserStorageService.Count;

        protected IUserStorageService UserStorageService { get; }

        public virtual void Add(User user)
        {
            UserStorageService.Add(user);
        }

        public virtual bool Remove(User user)
        {
            return UserStorageService.Remove(user);
        }

        public virtual User Search(string firstName)
        {
            return UserStorageService.Search(firstName);
        }

        public virtual User Search(Predicate<User> predicate)
        {
            return UserStorageService.Search(predicate);
        }

        public virtual IEnumerable<User> SearchAll(string firstName)
        {
            return UserStorageService.SearchAll(firstName);
        }

        public virtual IEnumerable<User> SearchAll(Predicate<User> predicate)
        {
            return UserStorageService.SearchAll(predicate);
        }

        public UserStorageServiceMode ServiceMode => UserStorageService.ServiceMode;
    }
}
