using FactoryDesignPattern.Models;

namespace FactoryDesignPattern.Factory
{
	public interface IQRCodeGenerator
	{
		byte[] GenerateQRCode(QRCodeModel model);
	}
}
