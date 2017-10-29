using System;
using UserStorageServices.Interfaces;

namespace UserStorageServices
{
    public class GeneratorGuid : IGeneratorId
    {
        public Guid Generate() => Guid.NewGuid();
    }
}
