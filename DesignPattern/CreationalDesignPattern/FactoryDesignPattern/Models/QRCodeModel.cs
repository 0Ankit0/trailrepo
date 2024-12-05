namespace FactoryDesignPattern.Models
{
	public class QRCodeModel
	{
		// Data to be encoded in the QR code (URL, phone number, etc.)
		public string? Data { get; set; }

		public string? PhoneNumber { get; set; }

		// Enum to specify the type of QR code (URL, SMS, Phone Number)
		public QRCodeType QRCodeType { get; set; }
	}

	public enum QRCodeType
	{
		URL,
		SMS,
		PhoneNumber
	}

}
