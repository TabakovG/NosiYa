using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NosiYa.Data.Models
{

    using Microsoft.AspNetCore.Identity;
    using Outfit;

    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.OutfitRenterDates = new HashSet<OutfitRenterDate>();
            this.Cart = new Cart();
        }

        public ICollection<OutfitRenterDate> OutfitRenterDates { get; set; }

        public Cart Cart { get; set; }

    }
}
