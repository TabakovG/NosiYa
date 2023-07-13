namespace NosiYa.Data.Models.Outfit
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;

    using Enums;
    using static Common.EntityValidationConstants.Outfit;

    public class OutfitPart
    {
        public OutfitPart()
        {
            this.Images = new HashSet<Image>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [MaxLength(DescriptionMaxLength)]
        public string? Description { get; set; }

        [Required]
        [MaxLength(ColorMaxLength)]
        public string Color { get; set; } = null!;

        [Required]
        public RenterType RenterType { get; set; }

        public OutfitPartType OutfitType { get; set; } 

        [Required]
        [ForeignKey(nameof(OutfitSet))]
        public int OutfitSetId { get; set; }

        public OutfitSet OutfitSet { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Owner))]
        public Guid OwnerId { get; set; }
        public ApplicationUser Owner { get; set; }

        [Required]
        [MaxLength(SizeMaxLength)]
        public string Size { get; set; } = null!;

        public ICollection<Image> Images { get; set; }

    }
}
