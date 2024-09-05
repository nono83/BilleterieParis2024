using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BilleterieParis2024.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }

        [Display(Name = "Commande")]
        public int OrderId { get; set; }

        [Display(Name = "Offre")]
        public int TicketsOfferID { get; set; }

        [ForeignKey("OrderId")]
        public Orders Order { get; set; }

        [ForeignKey("TicketsOfferID")]
        public virtual TicketsOffers TicketsOffer { get; set; }
       
    
    }
}
