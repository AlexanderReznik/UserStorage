namespace UserStorageServices.Validators
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
