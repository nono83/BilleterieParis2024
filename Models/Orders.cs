using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BilleterieParis2024.Models
{
    public class Orders
    {
        public Orders()
        {
            OrderDetails = new List<OrderDetails>();
        }
        public int Id { get; set; }

        [Required]
        [Display(Name = "Client")]
        public string UserId { get; set; }

        [Required]
        [Display(Name = "N° Commande")]
        public string OrderNo { get; set; }

        [Required]
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public string OrderKey { get; set; }

        public virtual List<OrderDetails> OrderDetails { get; set; }
    }
}
