﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorageServices.Interfaces;

namespace UserStorageServices
{
    public class UserStorageServiceLog : UserStorageServiceDecorator
    {
        private readonly BooleanSwitch _loggingSwitch = new BooleanSwitch("enableLogging", "Switch for logging");

        public UserStorageServiceLog(IUserStorageService service) : base(service)
        {
        }

        public override void Add(User user)
        {
            if (_loggingSwitch.Enabled)
            {
                Console.WriteLine("Add() method is called.");
            }
            base.Add(user);
        }

        public override bool Remove(User user)
        {
            if (_loggingSwitch.Enabled)
            {
                Console.WriteLine("Remove() method is called.");
            }
            return base.Remove(user);
        }

        public override User Search(string firstName)
        {
            if (_loggingSwitch.Enabled)
            {
                Console.WriteLine("Search() method is called.");
            }
            return base.Search(firstName);
        }

        public override User Search(Predicate<User> predicate)
        {
            if (_loggingSwitch.Enabled)
            {
                Console.WriteLine("Search() method is called.");
            }
            return base.Search(predicate);
        }

        public override IEnumerable<User> SearchAll(string firstName)
        {
            if (_loggingSwitch.Enabled)
            {
                Console.WriteLine("SearchAll() method is called.");
            }
            return base.SearchAll(firstName);
        }

        public override IEnumerable<User> SearchAll(Predicate<User> predicate)
        {
            if (_loggingSwitch.Enabled)
            {
                Console.WriteLine("SearchAll() method is called.");
            }
            return base.SearchAll(predicate);
        }
    }
}
