namespace UserStorageServices.IdGenerators
{
    public class GeneratorInt : IGeneratorId
    {
        public GeneratorInt()
        {
            LastId = 1000;
        }

        public int LastId { get; set; }

        public int Generate()
        {
            return ++LastId;
        }
    }
}
