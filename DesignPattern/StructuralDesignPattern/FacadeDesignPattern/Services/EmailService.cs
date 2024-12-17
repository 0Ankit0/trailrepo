namespace FacadeDesignPattern.Services
{
	public interface IEmailService
	{
		string SendEmail(int orderId, string productName);
	}

	public class EmailServices : IEmailService
	{
		public string SendEmail(int orderId, string productName)
		{
			return $"Email sent: Order #{orderId} for '{productName}' was created successfully.";
		}
	}

}
