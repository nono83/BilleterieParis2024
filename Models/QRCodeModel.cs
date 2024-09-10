namespace BilleterieParis2024.Models
{
    public class QRCodeModel
    {
        public int QRCodeType { get; set; }
        public string QRImageURL { get; set; }

        public string OrderNo { get; set; }

        public string UserName { get; set; }

        //for bookmark qr code
        public string BookmarkTitle { get; set; }
        public string BookmarkURL { get; set; }

        // for email qr codes
        public string ReceiverEmailAddress { get; set; }
        public string EmailSubject { get; set; }
        public string EmailMessage { get; set; }

        //for sms qr codes
        public string SMSPhoneNumber { get; set; }
        public string SMSBody { get; set; }
      
    }
}
