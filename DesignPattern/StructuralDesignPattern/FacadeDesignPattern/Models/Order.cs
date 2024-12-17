namespace FacadeDesignPattern.Models
{
	public class Order
	{
		public int OrderId { get; set; }
		public string? ProductName { get; set; }
		public int Quantity { get; set; }
		public bool SendEmail { get; set; }        
		public bool SendNotification { get; set; }

		public List<string> Messages { get; set; } = new List<string>();
	}

}
