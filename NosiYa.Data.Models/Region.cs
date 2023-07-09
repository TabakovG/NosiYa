namespace NosiYa.Data.Models
{

    using System.ComponentModel.DataAnnotations;

    using Outfit;
    using static NosiYa.Common.EntityValidationConstants.Region;

    public class Region
    {
        public Region()
        {
            this.Outfits = new HashSet<OutfitPart>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public ICollection<OutfitPart> Outfits { get; set; }
    }
}
