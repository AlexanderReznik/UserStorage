using UserStorageServices.Exceptions;

namespace UserStorageServices.Validators
{
    public class FirstNameValidator : IUserValidator
    {
        public void Validate(User user)
        {
            if (string.IsNullOrWhiteSpace(user.FirstName))
            {
                throw new FirstNameIsNullOrEmptyException("FirstName is null or empty or whitespace");
            }
        }
    }
}
