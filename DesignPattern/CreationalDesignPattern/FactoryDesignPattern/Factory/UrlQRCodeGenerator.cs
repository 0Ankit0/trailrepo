using FactoryDesignPattern.Factory;
using FactoryDesignPattern.Models;
using QRCoder;
using System.Drawing.Imaging;

namespace FactoryDesignPattern.Factory
{
	public class URLQRCodeGenerator : IQRCodeGenerator
	{
        public byte[] GenerateQRCode(QRCodeModel model)
        {
            using (var qrGenerator = new QRCodeGenerator())
            {
                // Create a QR code that encodes the URL
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(model.Data, QRCodeGenerator.ECCLevel.Q);
                using (var qrCode = new QRCode(qrCodeData))
                {
                    using (var ms = new MemoryStream())
                    {
                        qrCode.GetGraphic(20).Save(ms, ImageFormat.Png);
                        return ms.ToArray();
                    }
                }
            }
        }
    }
}
