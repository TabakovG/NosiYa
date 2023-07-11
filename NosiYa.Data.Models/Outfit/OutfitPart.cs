namespace NosiYa.Data.Models.Outfit
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;

    using Enums;
    using static Common.EntityValidationConstants.Outfit;
    using System.Drawing;

    public class OutfitPart
    {
        public OutfitPart()
        {
            this.Sizes = new HashSet<SizeType>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [MaxLength(DescriptionMaxLength)]
        public string? Description { get; set; }

        [ForeignKey(nameof(Region))]
        public int? RegionId { get; set; }
        public Region? Region { get; set; }

        [Required]
        [MaxLength(ColorMaxLength)]
        public string Color { get; set; } = null!;

        [Required]
        public RenterType RenterType { get; set; }


        [ForeignKey(nameof(OutfitSet))]
        public int? OutfitSetId { get; set; }

        public OutfitSet? OutfitSet { get; set; }

        public OutfitPartType Type { get; set; }

        public ICollection<SizeType> Sizes { get; set; }

    }
}
