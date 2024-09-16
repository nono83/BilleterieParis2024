using BilleterieParis2024.Data;
using BilleterieParis2024.Models;
using BilleterieParis2024.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.Security.Claims;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using static QRCoder.PayloadGenerator;
using System.Drawing.Printing;

using BilleterieParis2024.Services;


namespace BilleterieParis2024.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {
        private ApplicationDbContext _db;
        private Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;

        public OrderController(ApplicationDbContext db, Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        //GET Checkout action method

        public IActionResult Checkout()
        {
            return View();
        }

        //POST Checkout action method

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Checkout(Orders anOrder)
        {

            var user = await _userManager.GetUserAsync(User);
            var Message = $"Merci {user.FirstName} {user.LastName} !";


            List<TicketsOffers> ticketsOffers = HttpContext.Session.Get<List<TicketsOffers>>("TicketsOffers");

            foreach (var ticketsOffer in ticketsOffers)
            {
                OrderDetails orderDetails = new OrderDetails();
                orderDetails.TicketsOfferID = ticketsOffer.Id;
                anOrder.OrderDetails.Add(orderDetails);
            }
            //Récupération de l'id de l'utilisateur connecté
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var OrderNo = GetOrderNo();
            anOrder.OrderNo = OrderNo;
            anOrder.OrderKey = GetOrderKey(16);
            anOrder.UserId = userId;
            anOrder.OrderDate = DateTime.Now;

            _db.Orders.Add(anOrder);
            await _db.SaveChangesAsync();

            string QRCodeText = $"{user.CustomerKey} - {anOrder.OrderKey}";
            string userName= $"{user.FirstName} {user.LastName}";
            QRCodeModel qrCodeModel = CreateQRCode(QRCodeText,  anOrder.OrderNo, userName);

            //var orderResult = from order in _db.Orders
            //                  join orderDetail in _db.OrderDetails on order.Id equals orderDetail.OrderId
            //                  where order.OrderNo == anOrder.OrderNo
            //                  select new
            //                  {
            //                      OrderNo = order.OrderNo,
            //                      orderDate=order.OrderDate,
            //                      orderDetails = order.OrderDetails.ToList()
            //                  };
                         //select $"Commande n° {order.OrderNo}";
           // return RedirectToAction("CheckoutConfirm", anOrder);
            return View("CheckoutConfirm", qrCodeModel);

            // On réinitialise la liste des produits à 0
            HttpContext.Session.Set("TicketsOffers", new List<TicketsOffers>());

        }

        [Route("checkout-confirmation")]
        public async Task<IActionResult> CheckoutConfirm()
        {
            return View();
        }

        public string GetOrderNo()
        {
            int rowCount = _db.Orders.ToList().Count() + 1;
            return rowCount.ToString("000");
        }

        //Génération de la clé associée à la commande
        public  string GetOrderKey(int length)
        {
            const string chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

            StringBuilder sb = new StringBuilder();
            Random rnd = new Random();

            for (int i = 0; i < length; i++)
            {
                int index = rnd.Next(chars.Length);
                sb.Append(chars[index]);
            }

            return sb.ToString();
        }

        private QRCodeModel CreateQRCode(string QRCodeText, string OrderNo, string userName)
        {
            QRCodeModel model=new QRCodeModel();
            Payload payload = null;
            //payload = new Mail("arnaud.gianati@club-internet.fr", "Votre billet pour les jo", "Voici votre QRCode");
            payload = new SMS("0627818477", "Voici votre QRCode");
            QRCodeGenerator QrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = QrGenerator.CreateQrCode(QRCodeText, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            //GetGraphic retourne un bitmap qui peut être sauvegardé si on le souhaite
            var qrCodeAsBitmap = qrCode.GetGraphic(20);

            string base64String = Convert.ToBase64String(BitmapToByteArray(qrCodeAsBitmap));
            model.QRImageURL= "data:image/png;base64," + base64String;
            model.UserName = userName;
            model.OrderNo = OrderNo;

            return model;
        }

        //Convertion du bitmap en un tableau de byte après avoir été convertit en memory stream
        private byte[] BitmapToByteArray(Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms,System.Drawing.Imaging.ImageFormat.Png );
                //bitmap.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }

        public async Task<IActionResult> GeneratePDF(QRCodeModel qRCodeModel)
        {
            string logoUrl = $"https://{HttpContext.Request.Host.Value}/images/logo_jo_paris_2024.png";
            string htmlContent = "<div style='text-align: center; margin-bottom: 30px;'>";
            htmlContent += $"<img src ='{logoUrl}' height = '150px' width = '150px' />";
            htmlContent += "</div >";
            htmlContent += "<div style = 'text-align: center;' >";
            htmlContent += $" <div style = 'argin-bottom: 30px;' > Merci {qRCodeModel.UserName} pour votre commande </div >";
            htmlContent += $"<h3 style = 'margin-bottom: 30px;' > Commande n°{qRCodeModel.OrderNo} </h3 >";
            htmlContent += "<div style = 'margin-bottom: 30px;' ><p> Billets pour les jeux olympiques de Paris 2024.</p><p> Présentez le QrCode ci-dessous à l'entrée le jour des épreuves. </p></div >";
            htmlContent += $"<img src = '{qRCodeModel.QRImageURL}' height = '200px' width = '200px' />";
            htmlContent += "</div>";

             byte[] pdf = PDFService.CreateWithSelectPdf(htmlContent);
            // byte[] pdf = PDFService.CreatePdfWithPdfSharp(htmlContent);
            // byte[] pdf = PDFService.CreatePdfWithIronPdf(htmlContent);
            //byte[] pdf = PDFService.CreateWithDinkToPdf(htmlContent);

            string fileName = $"billetjo2024-{qRCodeModel.UserName}-num{qRCodeModel.OrderNo}.pdf";

            return File(pdf, "application/pdf", fileName);
        }
    }
}
