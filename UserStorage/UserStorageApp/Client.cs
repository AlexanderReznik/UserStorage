using System;
using System.Collections.Generic;
using UserStorageServices;
using UserStorageServices.Interfaces;
using UserStorageServices.Services;

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
            _userStorageService = service ?? new UserStorageServiceLog(new UserStorageServiceMaster());
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

            var slave1 = new UserStorageServiceLog(new UserStorageServiceSlave());
            var slave2 = new UserStorageServiceSlave();
            var slave3 = new UserStorageServiceSlave();

            var service = new UserStorageServiceMaster(slaves: new List<IUserStorageService>()
            {
                slave1,
                slave2
            });
            service.AddSubscriber(slave3);
            var master = new UserStorageServiceLog(service);
            
            master.Add(alex);

            Console.WriteLine(slave3.Search(u => u.FirstName == "Alex").LastName);

            master.Remove(alex);
        }
    }
}
