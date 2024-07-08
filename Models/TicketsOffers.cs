using System.ComponentModel.DataAnnotations;

namespace BilleterieParis2024.Models
{
    public class TicketsOffers
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Le nom est obligatoire")]
        [Display(Name = "Nom de l'offre")]
        public string OfferName { get; set; }

        [Required(ErrorMessage ="Le prix est obligatoire")]
        [RegularExpression("^\\d+$", ErrorMessage = "Le prix doit être un nombre entier positif")]
        [Display(Name = "Prix")]

        public int Price { get; set; }

    }
}
