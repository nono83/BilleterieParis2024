using BilleterieParis2024.Models;
//using DinkToPdf;
//using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;
using SelectPdf;
//using PdfSharpCore;
//using PdfSharpCore.Pdf;
//using TheArtOfDev.HtmlRenderer.PdfSharp;
//using IronPdf;


namespace BilleterieParis2024.Services
{
    public  class PDFService
    {
        //private readonly IConverter _converter;

        //public  PDFService(IConverter converter)
        //{
        //    _converter = converter;
        //}

        //public static byte[] CreateWithDinkToPdf(string htmlContent)
        //{
        //    var globalSettings = new GlobalSettings
        //    {
        //        ColorMode = ColorMode.Color,
        //        Orientation = Orientation.Portrait,
        //        PaperSize = PaperKind.A4,
        //        Margins = new MarginSettings { Top = 18, Bottom = 18 },
        //    };

        //    var objectSettings = new ObjectSettings
        //    {
        //        PagesCount = true,
        //        HtmlContent = htmlContent,
        //        WebSettings = { DefaultEncoding = "utf-8" },
        //        HeaderSettings = { FontSize = 10, Right = "Page [page] of [toPage]", Line = true },
        //        FooterSettings = { FontSize = 8, Center = "PDF demo from JeminPro", Line = true },
        //    };

        //    var htmlToPdfDocument = new HtmlToPdfDocument()
        //    {
        //        GlobalSettings = globalSettings,
        //        Objects = { objectSettings },
        //    };

        //    return _converter.Convert(htmlToPdfDocument);
        //}

        public static byte[] CreateWithSelectPdf(string htmlContent)
        {
            HtmlToPdf converter = new HtmlToPdf();
            //PdfDocument doc = converter.ConvertUrl("https://selectpdf.com");
            PdfDocument doc = converter.ConvertHtmlString(htmlContent);
            //doc.Save("test.pdf");
            byte[] pdf = doc.Save();
            doc.Close();

            return pdf;
        }

        //public static byte[] CreatePdfWithPdfSharp(string htmlContent)
        //{
        //    // Create a new PdfDocument object
        //    var pdfDoc = new PdfDocument();

        //    PdfGenerator.AddPdfPages(pdfDoc, htmlContent, PageSize.A4);
        //    byte[]? response = null;
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        pdfDoc.Save(ms);
        //        response = ms.ToArray();
        //    }

        //    return response;
        //}


        //public static byte[] CreatePdfWithIronPdf(string htmlContent)
        //{
        //    var Renderer = new ChromePdfRenderer(); // Instantiates Chrome Renderer
        //    var pdf = Renderer.RenderHtmlAsPdf(htmlContent);

        //    //    return new FileStreamResult(pdf.Stream, "application/pdf")
        //    //    {
        //    //        FileDownloadName = fileName
        //    //    };

        //    return pdf.BinaryData;
        //}
    }
}
