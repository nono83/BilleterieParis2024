namespace BilleterieParis2024.Models
{
    public class EmailModel
    {
        public string? From { get; set; } = "arnaud.gianati@club-internet.fr";
        public string? ToEmail { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public bool IsHtml { get; set; } = true;
    }
}
