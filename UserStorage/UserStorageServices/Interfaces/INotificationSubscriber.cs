namespace UserStorageServices.Interfaces
{
    public interface INotificationSubscriber
    {
        void UserAdded(object sender, object user);

        void UserRemoved(object sender, int id);
    }
}
