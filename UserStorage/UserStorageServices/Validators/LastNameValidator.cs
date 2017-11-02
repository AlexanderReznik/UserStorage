using UserStorageServices.Exceptions;
using UserStorageServices.Interfaces;

namespace UserStorageServices.Validators
{
    public class LastNameValidator : IUserValidator
    {
        public void Validate(User user)
        {
            if (string.IsNullOrWhiteSpace(user.LastName))
            {
                throw new LastNameIsNullOrEmptyException("LastName is null or empty or whitespace");
            }
        }
    }
}
