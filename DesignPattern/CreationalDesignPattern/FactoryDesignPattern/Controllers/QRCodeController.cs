using FactoryDesignPattern.Factory;
using FactoryDesignPattern.Models;
using Microsoft.AspNetCore.Mvc;

namespace FactoryDesignPattern.Controllers
{
	public class QRCodeController : Controller
	{
		public IActionResult Index()
		{
			return View(new QRCodeModel());
        }

        [HttpPost]
        public IActionResult GenerateQRCode([FromForm] QRCodeModel model)
        {
            if (ModelState.IsValid)
            {
                // Get the appropriate QR code generator based on the requested type
                IQRCodeGenerator qrCodeGenerator = QRCodeGeneratorFactory.GetQRCodeGenerator(model.QRCodeType);

                // Generate the QR code as a byte array
                byte[] qrCodeImage = qrCodeGenerator.GenerateQRCode(model);

                // Add the QR code image to the model so it can be passed back to the view
                ViewData["QRCodeImage"] = qrCodeImage;
                return View("Index", model);  // Return to the view with the model and QR code
            }

            // If the model is invalid, return the same view with the current model
            return View("Index", model);
        }
    }
}
