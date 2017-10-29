using System;
using UserStorageServices.Exceptions;
using UserStorageServices.Interfaces;

namespace UserStorageServices.Validators
{
    class AgeValidator : IUserValidator
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
