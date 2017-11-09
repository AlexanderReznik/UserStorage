﻿using System;
using System.Collections.Generic;
using System.Linq;
using UserStorageServices.IdGenerators;

namespace UserStorageServices.Repositories
{
    public class DefaultUserRepository : IUserRepository
    {
        public DefaultUserRepository(IGeneratorId generatorId = null)
        {
            List = new List<User>();
            GeneratorId = generatorId ?? new GeneratorInt();
        }

        public int Count => List.Count;

        protected List<User> List { get; set; }

        protected IGeneratorId GeneratorId { get; }

        public virtual void Start()
        {
        }

        public virtual void Stop()
        {
        }

        public User Get(int id)
        {
            return List.Find(u => u.Id == id);
        }

        public void Set(User user)
        {
            if (user.Id == null)
            {
                user.Id = GeneratorId.Generate();
            }

            List.Add(user);
        }

        public bool Delete(int id)
        {
            var listUser = List.Find(user => user.Id == id);
            if (listUser == null)
            {
                return false;
            }

            return List.Remove(listUser);
        }

        public IEnumerable<User> Query(Predicate<User> predicate)
        {
            return List.FindAll(predicate);
        }
    }
}