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
            _userStorageService = service ?? new UserStorageService();
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
        }
    }
}
