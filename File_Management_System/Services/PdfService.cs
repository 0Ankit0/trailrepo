using File_Management_System.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using iText.Html2pdf;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;

namespace File_Management_System.Services
{
    public class PdfService
    {
        private readonly ICompositeViewEngine _viewEngine;
        private readonly ITempDataProvider _tempDataProvider;
        private readonly IFileProvider _fileProvider;

        public PdfService(ICompositeViewEngine viewEngine, ITempDataProvider tempDataProvider, IFileProvider fileProvider)
        {
            _viewEngine = viewEngine;
            _tempDataProvider = tempDataProvider;
            _fileProvider = fileProvider;
        }

        public byte[] GeneratePDF<TModel>(string viewName, TModel model, ControllerContext controllerContext)
        {
            // Render Razor view to HTML string dynamically
            string htmlContent = RenderViewToString(viewName, model, controllerContext);

            // Convert HTML to PDF using iText7
            using (MemoryStream pdfStream = new MemoryStream())
            {
                HtmlConverter.ConvertToPdf(htmlContent, pdfStream);
                return pdfStream.ToArray();
            }
        }

        private string RenderViewToString<TModel>(string viewName, TModel model, ControllerContext controllerContext)
        {
            var viewEngineResult = _viewEngine.FindView(controllerContext, viewName, isMainPage: false);

            if (!viewEngineResult.Success || viewEngineResult.View == null)
            {
                throw new FileNotFoundException($"View '{viewName}' not found.");
            }

            var viewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
            {
                Model = model
            };

            var tempData = new TempDataDictionary(controllerContext.HttpContext, _tempDataProvider);

            using (var sw = new StringWriter())
            {
                var viewContext = new ViewContext(
                    controllerContext,
                    viewEngineResult.View,
                    viewData,
                    tempData,
                    sw,
                    new HtmlHelperOptions()
                );

                viewEngineResult.View.RenderAsync(viewContext).Wait();
                return sw.ToString();
            }
        }
    }
}
