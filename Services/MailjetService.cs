//using Mailjet.Client;
//using BilleterieParis2024.Models;
//using Newtonsoft.Json.Linq;
//using Mailjet.Client.Resources;
//using Microsoft.Extensions.Options;
//using Mailjet.Client.TransactionalEmails;
//using Microsoft.AspNetCore.Identity.UI.Services;

namespace BilleterieParis2024.Services
{
//    public class MailjetService : IEmailSender
//    {
//        private readonly ILogger<MailjetService> _logger;
//        private readonly IConfiguration _config;

//        public MailjetService(ILogger<MailjetService> logger, IConfiguration config)
//        {
//            _logger = logger;
//            _config = config;
//        }

//        public async Task SendEmailAsync(string toEmail, string subject, string body)
//        {
//            try
//            {
//                string apiKey = _config["Mailjet:MailjetApiKey"];
//                string secretKey = _config["Mailjet:MailjetSecretKey"];
//                string senderEmail = _config["Mailjet:MailjetEmail"];

//                MailjetClient client = new MailjetClient(apiKey, secretKey);

//                MailjetRequest request = new MailjetRequest
//                {
//                    Resource = Send.Resource
//                };

//                var email = new TransactionalEmailBuilder()
//                       .WithFrom(new SendContact(senderEmail))
//                       .WithSubject(subject)
//                       .WithHtmlPart(body)
//                       .WithTo(new SendContact(toEmail))
//                       .Build();

//                var response = await client.SendTransactionalEmailAsync(email);
//                var message = response.Messages[0];

//                bool result = message.Status.ToLower() == "success";

//                _logger.LogInformation("Email sent successfully.");

//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error sending email.");


//            }
//        }


//    }

//        2e methode avec JSON
//        public MailjetService(IOptions<AuthMessageSenderOptions> optionsAccessor,
//                       ILogger<EmailSender> logger)
//    {
//        Options = optionsAccessor.Value;
//        _logger = logger;
//    }


//    public AuthMessageSenderOptions Options { get; } //Set with Secret Manager.
//    static async Task RunAsync()
//    {
//        //MailjetClient client = new MailjetClient(Environment.GetEnvironmentVariable("MJ_APIKEY_PUBLIC"), Environment.GetEnvironmentVariable("MJ_APIKEY_PRIVATE"));
//        MailjetClient client = new MailjetClient(Options.MJ_APIKEY_PUBLIC, Options.MJ_APIKEY_PUBLIC);

//        MailjetRequest request = new MailjetRequest
//        {
//            Resource = Send.Resource,
//        }
//           .Property(Send.Messages, new JArray {
//                            new JObject {
//                             {"From", new JObject {
//                              {"Email", "pilot@mailjet.com"},
//                              {"Name", "Mailjet Pilot"}
//                              }},
//                             {"To", new JArray {
//                              new JObject {
//                               {"Email", "passenger1@mailjet.com"},
//                               {"Name", "passenger 1"}
//                               }
//                              }},
//                             {"Subject", "Your email flight plan!"},
//                             {"TextPart", "Dear passenger 1, welcome to Mailjet! May the delivery force be with you!"},
//                             {"HTMLPart", "<h3>Dear passenger 1, welcome to <a href=\"https://www.mailjet.com/\">Mailjet</a>!</h3><br />May the delivery force be with you!"}
//                             }
//               });
//        MailjetResponse response = await client.PostAsync(request);
//    }
}
