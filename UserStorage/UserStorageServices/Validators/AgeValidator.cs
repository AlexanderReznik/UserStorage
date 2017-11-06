using UserStorageServices.Exceptions;

namespace UserStorageServices.Validators
{
    public class AgeValidator : IUserValidator
    {
        public void Validate(User user)
        {
            if (user.Age < 0)
            {
                throw new AgeExceedsLimitsException("Age is incorrect");
            }
        }
    }
}
