using Microsoft.AspNetCore.Identity;

namespace BilleterieParis2024.Models
{
    public class ApplicationUser: IdentityUser
    {
        public ApplicationUser()
        {
            Orders = new List<Orders>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CustomerKey { get; set; }

        public virtual List<Orders> Orders { get; set; }
    }
}
