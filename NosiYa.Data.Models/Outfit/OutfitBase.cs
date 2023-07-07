namespace NosiYa.Data.Models.Outfit
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;

    using Enums;
    using static NosiYa.Common.EntityValidationConstants.OutfitBase;

    public abstract class OutfitBase
    {

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

        public decimal PricePerDay { get; set; }

        [Required]
        [MaxLength(ColorMaxLength)]
        public string Color { get; set; } = null!;

        [Required]
        [MaxLength(PictureMaxLength)]
        public string Picture { get; set; } = null!;

        [Required]
        public RenterType RenterType { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        [ForeignKey(nameof(OutfitSet))]
        public int? OutfitSetId { get; set; }

        public OutfitSet? OutfitSet { get; set; }

        public ICollection<OutfitRenterDate> OutfitRenterDates { get; set; } = new HashSet<OutfitRenterDate>();

    }
}
