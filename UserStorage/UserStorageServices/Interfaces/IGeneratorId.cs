using System;

namespace UserStorageServices.Interfaces
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
