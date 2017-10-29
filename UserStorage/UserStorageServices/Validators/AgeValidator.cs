using System;
using UserStorageServices.Interfaces;

namespace UserStorageServices.Validators
{
    class AgeValidator : IUserValidator
    {
        public void Validate(User user)
        {
            if (user.Age < 0)
            {
                throw new ArgumentException("Age is incorrect", nameof(user));
            }
        }
    }
}
