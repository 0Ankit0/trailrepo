using FactoryDesignPattern.Models;
namespace FactoryDesignPattern.Factory
{
	public class QRCodeGeneratorFactory
	{
		public static IQRCodeGenerator GetQRCodeGenerator(QRCodeType type)
		{
			switch (type)
			{
				case QRCodeType.SMS:
					return new SMSQRCodeGenerator();
				case QRCodeType.PhoneNumber:
					return new PhoneNumberQRCodeGenerator();
				case QRCodeType.URL:
				default:
					return new URLQRCodeGenerator();
			}
		}
	}
}
