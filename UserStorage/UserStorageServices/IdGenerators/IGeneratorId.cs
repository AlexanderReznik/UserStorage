namespace UserStorageServices.IdGenerators
{
    public interface IGeneratorId
    {
        int LastId { get; set; }

        /// <summary>
        /// Generates id
        /// </summary>
        /// <returns>Generated id</returns>
        int Generate();
    }
}
