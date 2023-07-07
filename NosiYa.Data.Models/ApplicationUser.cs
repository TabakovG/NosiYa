namespace NosiYa.Data.Models
{

    using Microsoft.AspNetCore.Identity;
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.OutfitRenterDates = new HashSet<OutfitRenterDate>();
        }

        public ICollection<OutfitRenterDate> OutfitRenterDates { get; set; }

    }
}
