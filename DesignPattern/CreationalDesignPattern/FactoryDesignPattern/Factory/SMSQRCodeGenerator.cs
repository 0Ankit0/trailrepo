using QRCoder;
using System.IO;
using FactoryDesignPattern.Models;

namespace FactoryDesignPattern.Factory
{
    public class SMSQRCodeGenerator : IQRCodeGenerator
    {
        public byte[] GenerateQRCode(QRCodeModel model)
        {
            // SMS format: "sms:<phone_number>?body=<message>"
            string smsUri = $"sms:{model.PhoneNumber}?body={model.Data}";

            // Create a new QRCodeGenerator object
            QRCodeGenerator qrGenerator = new QRCodeGenerator();

            // Generate the QR code data from the SMS URI string
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(smsUri, QRCodeGenerator.ECCLevel.L);

            // Create a QRCode object from the QR code data
            QRCode qrCode = new QRCode(qrCodeData);

            // Get the QR code as a byte array in PNG format
            using (MemoryStream ms = new MemoryStream())
            {
                qrCode.GetGraphic(20).Save(ms, System.Drawing.Imaging.ImageFormat.Png); // 20 is the pixel size for each QR code module
                return ms.ToArray(); // Return the byte array of the PNG image
            }
        }
    }
}
