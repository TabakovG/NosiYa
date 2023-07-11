namespace NosiYa.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Outfit;

    public class Cart
    {

        public Cart()
        {
            this.Outfits = new HashSet<OutfitForCart>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Owner))]
        public Guid OwnerId { get; set; }
        public ApplicationUser Owner { get; set; } = null!;

        public ICollection<OutfitForCart> Outfits { get; set; }

    }
}
