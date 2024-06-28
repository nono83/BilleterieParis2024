using Microsoft.AspNetCore.Identity;

namespace BilleterieParis2024.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CustomerKey { get; set; }

    }
}
