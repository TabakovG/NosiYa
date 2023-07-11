using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NosiYa.Data.Models.Outfit;

namespace NosiYa.Data.Models
{

    public  class Cart
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
        public ApplicationUser Owner { get; set; }

        public ICollection<OutfitForCart> Outfits { get; set; }

    }
}
