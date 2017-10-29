using System;
using System.Collections.Generic;
using UserStorageServices.Interfaces;

namespace UserStorageServices.Validators
{
    public class DefaultUserValidator : IUserValidator
    {
        public DefaultUserValidator()
        {
            ValidatorList = new List<IUserValidator>();
            ValidatorList.Add(new FirstNameValidator());
            ValidatorList.Add(new LastNameValidator());
            ValidatorList.Add(new AgeValidator());
        }

        private List<IUserValidator> ValidatorList { get; }

        public void Validate(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            foreach (var validator in ValidatorList)
            {
                validator.Validate(user);
            }
        }
    }
}
