namespace UserStorageServices.Notifications
{
    public interface INotificationSender
    {
        void Send(NotificationContainer container);

        void AddReceiver(INotificationReceiver receiver);
    }
}
