﻿using System;
using System.Collections.Generic;

namespace UserStorageServices.Interfaces
{
    public interface IUserStorageService
    {
        int Count { get; }

        void Add(User user);

        bool Remove(User user);

        User Search(string firstName);

        User Search(Predicate<User> predicate);

        IEnumerable<User> SearchAll(string firstName);

        IEnumerable<User> SearchAll(Predicate<User> predicate);
    }
}