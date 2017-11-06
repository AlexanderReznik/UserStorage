using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorageServices.Interfaces;

namespace UserStorageServices.IdGenerators
{
    class GeneratorInt : IGeneratorId
    {
        public int LastId { get; set; }

        public GeneratorInt()
        {
            LastId = 1000;
        }

        public int Generate()
        {
            return ++LastId;
        }
    }
}
