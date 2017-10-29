using System;
using UserStorageServices.Interfaces;

namespace UserStorageServices.Validators
{
    class FirstNameValidator : IUserValidator
    {
        public void Validate(User user)
        {
            if (string.IsNullOrWhiteSpace(user.FirstName))
            {
                throw new ArgumentException("FirstName is null or empty or whitespace", nameof(user));
            }
        }
    }
}
