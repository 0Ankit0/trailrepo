using File_Management_System.Models;
using File_Management_System.Services;
using Microsoft.AspNetCore.Mvc;

namespace File_Management_System.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly PdfService _pdfService;

        public InvoiceController(PdfService pdfService)
        {
            _pdfService = pdfService;
        }

        public IActionResult DownloadInvoice(int id)
        {
            var invoice = new Invoice
            {
                InvoiceNumber = "INV-DOTNET-1001",
                Date = DateTime.Now,
                CustomerName = "Pranaya Rout",
                Items = new List<InvoiceItem>
                {
                    new InvoiceItem { ItemName = "Item 1", Quantity = 2, UnitPrice = 15.0m },
                    new InvoiceItem { ItemName = "Item 2", Quantity = 3, UnitPrice = 10.0m },
                    new InvoiceItem { ItemName = "Item 3", Quantity = 1, UnitPrice = 35.0m }
                },
                PaymentMode = "COD"
            };
            //Set the Total Amount
            invoice.TotalAmount = invoice.Items.Sum(x => x.TotalPrice);
            var viewName = "InvoicePrint";

            // Generate PDF
            byte[] pdfBytes = _pdfService.GeneratePDF(viewName, invoice, ControllerContext);

            // Return PDF as a downloadable file
            //return File(pdfBytes, "application/pdf", "Invoice.pdf");

            // Return PDF as an inline file
            Response.Headers.Add("Content-Disposition", "inline; filename=Invoice.pdf");
            return File(pdfBytes, "application/pdf");
        }
    }

}
