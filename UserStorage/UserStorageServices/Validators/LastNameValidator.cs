using System;
using UserStorageServices.Interfaces;

namespace UserStorageServices.Validators
{
    class LastNameValidator : IUserValidator
    {
        public void Validate(User user)
        {
            if (string.IsNullOrWhiteSpace(user.LastName))
            {
                throw new ArgumentException("LastName is null or empty or whitespace", nameof(user));
            }
        }
    }
}
