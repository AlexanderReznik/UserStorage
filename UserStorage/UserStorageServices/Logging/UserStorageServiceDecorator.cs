using System;
using System.Collections.Generic;
using UserStorageServices.Services;

namespace UserStorageServices.Logging
{
    public abstract class UserStorageServiceDecorator : MarshalByRefObject, IUserStorageService
    {
        protected UserStorageServiceDecorator(IUserStorageService service)
        {
            UserStorageService = service ?? new UserStorageServiceSlave();
        }

        public int Count => UserStorageService.Count;

        public UserStorageServiceMode ServiceMode => UserStorageService.ServiceMode;

        protected IUserStorageService UserStorageService { get; }

        public virtual void Add(User user)
        {
            UserStorageService.Add(user);
        }

        public virtual bool Remove(int? id)
        {
            return UserStorageService.Remove(id);
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
    }
}
