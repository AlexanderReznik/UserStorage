using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UserStorageServices;
using UserStorageServices.Interfaces;

namespace UserStorageApp
{
    /// <summary>
    /// Represents a client that uses an instance of the <see cref="UserStorageService"/>.
    /// </summary>
    public class Client
    {
        private readonly IUserStorageService _userStorageService;

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        public Client(IUserStorageService service = null)
        {
            _userStorageService = service ?? new UserStorageServiceLog(new UserStorageService());
        }

        /// <summary>
        /// Runs a sequence of actions on an instance of the <see cref="UserStorageService"/> class.
        /// </summary>
        public void Run()
        {
            var alex = new User
            {
                FirstName = "Alex",
                LastName = "Black",
                Age = 25
            };

            _userStorageService.Add(alex);

            _userStorageService.Search("Alex");

            _userStorageService.Remove(alex);

            Console.WriteLine("And now something useful");

            var slave1 = new UserStorageServiceLog(new UserStorageService(UserStorageServiceMode.SlaveNode));
            var slave2 = new UserStorageService(UserStorageServiceMode.SlaveNode);
            var slave3 = new UserStorageService(UserStorageServiceMode.SlaveNode);

            var master = new UserStorageService(mode : UserStorageServiceMode.MasterNode, slaves : new IUserStorageService[] {slave1, slave2});
            master.AddSubscriber(slave3);

            master.Add(alex);

            Console.WriteLine(slave3.Search(u => u.FirstName == "Alex").LastName);

            //slave2.Add(alex);

            master.Remove(alex);

            //slave1.Remove(alex);
        }
    }
}
