using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices
{
    public interface IGeneratorId
    {
        /// <summary>
        /// Generates id
        /// </summary>
        /// <returns>Generated id</returns>
        Guid Generate();
    }
}
