using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BilleterieParis2024.Models
{
    [Bind("Id,OfferName,Price,IsAvailable")]
    public class TicketsOffers
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Le nom est obligatoire")]
        [Display(Name = "Nom de l'offre")]
        public string OfferName { get; set; }

        [BindProperty]
        public string? Description { get; set; }

        [Required(ErrorMessage ="Le prix est obligatoire")]
        [RegularExpression("^\\d+$", ErrorMessage = "Le prix doit être un nombre entier positif")]
        [Display(Name = "Prix")]
        public int Price { get; set; }


        public string? Image { get; set; }


        [Display(Name = "Disponible")]
        public bool IsAvailable { get; set; }


    }
}
