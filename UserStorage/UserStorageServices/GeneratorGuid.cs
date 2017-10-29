using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices
{
    public class GeneratorGuid : IGeneratorId
    {
        public Guid Generate() => Guid.NewGuid();
    }
}
