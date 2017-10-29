using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices
{
    public interface IUserValidator
    {
        /// <summary>
        /// A method for validating user.
        /// </summary>
        /// <param name="user">User to validate.</param>
        void Validate(User user);
    }
}
