namespace NosiYa.Data.Models
{

    using System.ComponentModel.DataAnnotations;

    using Outfit;
    using static NosiYa.Common.EntityValidationConstants.Region;

    public class Region
    {
        public Region()
        {
            this.Outfits = new HashSet<OutfitSet>();
            this.Images = new HashSet<Image>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        public ICollection<Image> Images { get; set; }
 
        public ICollection<OutfitSet> Outfits { get; set; }
    }
}
