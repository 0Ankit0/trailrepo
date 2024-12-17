namespace FacadeDesignPattern.Services
{
	public interface INotificationService
	{
		string SendNotification(int orderId, string productName);
	}

	public class NotificationServices : INotificationService
	{
		public string SendNotification(int orderId, string productName)
		{
			return $"Notification sent: Order #{orderId} for '{productName}' has been created.";
		}
	}

}
