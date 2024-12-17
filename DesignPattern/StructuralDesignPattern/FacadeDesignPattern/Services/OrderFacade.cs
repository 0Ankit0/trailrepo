using FacadeDesignPattern.Models;

namespace FacadeDesignPattern.Services
{
	public class OrderFacade
	{
		private readonly IEmailService _emailService;
		private readonly INotificationService _notificationService;

		public OrderFacade(IEmailService emailService, INotificationService notificationService)
		{
			_emailService = emailService;
			_notificationService = notificationService;
		}

		public void ProcessOrder(Order order)
		{
			order.Messages.Add($"Order Created: ID = {order.OrderId}, Product = {order.ProductName}, Quantity = {order.Quantity}");

			// Send email if selected
			if (order.SendEmail)
			{
				string emailMessage = _emailService.SendEmail(order.OrderId, order.ProductName);
				order.Messages.Add(emailMessage);
			}

			// Send notification if selected
			if (order.SendNotification)
			{
				string notificationMessage = _notificationService.SendNotification(order.OrderId, order.ProductName);
				order.Messages.Add(notificationMessage);
			}
		}
	}


}
