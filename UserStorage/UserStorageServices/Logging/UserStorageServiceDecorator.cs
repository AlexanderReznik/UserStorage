using System;
using System.Collections.Generic;
using UserStorageServices.Services;

namespace UserStorageServices.Logging
{
    public abstract class UserStorageServiceDecorator : MarshalByRefObject, IUserStorageService
    {
        protected UserStorageServiceDecorator(IUserStorageService service)
        {
            this.UserStorageService = service ?? new UserStorageServiceSlave();
        }

        public int Count => this.UserStorageService.Count;

        public UserStorageServiceMode ServiceMode => this.UserStorageService.ServiceMode;

        protected IUserStorageService UserStorageService { get; }

        public virtual void Add(User user)
        {
            this.UserStorageService.Add(user);
        }

        public virtual bool Remove(int? id)
        {
            return this.UserStorageService.Remove(id);
        }

        public virtual User Search(string firstName)
        {
            return this.UserStorageService.Search(firstName);
        }

        public virtual User Search(Predicate<User> predicate)
        {
            return this.UserStorageService.Search(predicate);
        }

        public virtual IEnumerable<User> SearchAll(string firstName)
        {
            return this.UserStorageService.SearchAll(firstName);
        }

        public virtual IEnumerable<User> SearchAll(Predicate<User> predicate)
        {
            return this.UserStorageService.SearchAll(predicate);
        }
    }
}
