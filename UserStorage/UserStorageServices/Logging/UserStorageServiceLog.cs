using System;
using System.Collections.Generic;
using System.Diagnostics;
using UserStorageServices.Services;

namespace UserStorageServices.Logging
{
    public class UserStorageServiceLog : UserStorageServiceDecorator
    {
        private readonly BooleanSwitch _loggingSwitch = new BooleanSwitch("enableLogging", "Switch for logging");

        public UserStorageServiceLog(IUserStorageService service) : base(service)
        {
        }

        public override void Add(User user)
        {
            if (this._loggingSwitch.Enabled)
            {
                Trace.WriteLine("Add() method is called.");
            }

            base.Add(user);
        }

        public override bool Remove(int? id)
        {
            if (this._loggingSwitch.Enabled)
            {
                Trace.WriteLine("Remove() method is called.");
            }

            return base.Remove(id);
        }

        public override User Search(string firstName)
        {
            if (this._loggingSwitch.Enabled)
            {
                Trace.WriteLine("Search() method is called.");
            }

            return base.Search(firstName);
        }

        public override User Search(Predicate<User> predicate)
        {
            if (this._loggingSwitch.Enabled)
            {
                Trace.WriteLine("Search() method is called.");
            }

            return base.Search(predicate);
        }

        public override IEnumerable<User> SearchAll(string firstName)
        {
            if (this._loggingSwitch.Enabled)
            {
                Trace.WriteLine("SearchAll() method is called.");
            }

            return base.SearchAll(firstName);
        }

        public override IEnumerable<User> SearchAll(Predicate<User> predicate)
        {
            if (this._loggingSwitch.Enabled)
            {
                Trace.WriteLine("SearchAll() method is called.");
            }

            return base.SearchAll(predicate);
        }
    }
}
