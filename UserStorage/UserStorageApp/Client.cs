using System;
using System.Configuration;
using UserStorageServices;
using UserStorageServices.Logging;
using UserStorageServices.Repositories;
using UserStorageServices.Services;

namespace UserStorageApp
{
    /// <summary>
    /// Represents a client that uses an instance of the <see cref="UserStorageService"/>.
    /// </summary>
    public class Client : MarshalByRefObject
    {
        private readonly IUserStorageService _userStorageService;
        private readonly IUserRepositoryManager _userRepositoryManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        public Client(IUserStorageService service = null, IUserRepositoryManager repository = null)
        {
            var path = ReadSetting("SavePath");
            var rep = new UserRepositoryWithState(path);
            _userRepositoryManager = repository ?? rep;
            _userStorageService = service ?? new UserStorageServiceLog(new UserStorageServiceMaster(repository: rep));
        }

        /// <summary>
        /// Runs a sequence of actions on an instance of the <see cref="UserStorageService"/> class.
        /// </summary>
        public void Run()
        {
            var alex = new User
            {
                FirstName = "Alex",
                LastName = "Star",
                Age = 25
            };

            _userRepositoryManager.Start();

            _userStorageService.Add(alex);
            _userStorageService.Search(u => u.LastName == "Star");
            _userStorageService.Remove(15);

            _userRepositoryManager.Stop();
        }

        private static string ReadSetting(string key)
        {
            var appSettings = ConfigurationManager.AppSettings;
            string path = appSettings[key];
            return path;
        }
    }
}
