using System.ComponentModel.DataAnnotations;

namespace BilleterieParis2024.Models
{
    public class PaymentCard
    {
        [Required(ErrorMessage = "Le numéro de carte est obligatoire")]
        [Display(Name = "Numéro de carte")]
        [RegularExpression(@"(\d{16})", ErrorMessage = "Le numéro de carte doit avoir 16 chiffres")]
        public string CardNo { get; set; }

        [Required(ErrorMessage = "Le cryptogramme est obligatoire")]
        [Display(Name = "Cryptogramme visuel")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Le cryptogramme doit avoir 3 chiffres")]
        [Range(0, 999, ErrorMessage = "Le cryptogramme {0} doit être compris entre {1} et {2}.")]
        public int Crypto { get; set; }


        [Display(Name = "Date d'expiration")]
        [Required(ErrorMessage = "La date d'expiration est obligatoire")]
        [RegularExpression(@"(0[1-9]|1[0-2])\/\d{2}", ErrorMessage = "La date doit avoir le format mm/yy")]
        [StringLength(5)]
        //[DisplayFormat(DataFormatString = "{0:MM/yy}", ApplyFormatInEditMode = true)]
        public string ExpirationDate { get; set; }


    }
}
