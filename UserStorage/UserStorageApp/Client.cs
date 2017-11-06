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
    public class Client
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
            _userStorageService.Remove(alex);

            _userRepositoryManager.Stop();

            /*Console.WriteLine("And now something useful");

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

            master.Remove(alex);*/
        }

        private static string ReadSetting(string key)
        {
            var appSettings = ConfigurationManager.AppSettings;
            string path = appSettings[key];
            return path;
        }
    }
}
