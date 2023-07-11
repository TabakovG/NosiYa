namespace NosiYa.Data.Models.Outfit
{
    using Enums;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static Common.EntityValidationConstants.Outfit;


    public class OutfitSet
    {
        public OutfitSet()
        {
            this.Sizes = new HashSet<SizeType>();
            this.Images = new HashSet<string>();
            this.OutfitParts = new HashSet<OutfitPart>();
            this.OutfitRenterDates = new HashSet<OutfitRenterDate>();
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

        public decimal PricePerDay { get; set; }

        [Required]
        [MaxLength(ColorMaxLength)]
        public string Color { get; set; } = null!;

        [Required]
        [MaxLength(ImageMaxLength)]
        public ICollection<string> Images { get; set; } = null!;

        [Required]
        public RenterType RenterType { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        public ICollection<SizeType> Sizes { get; set; }

        public ICollection<OutfitPart> OutfitParts { get; set; }

        public ICollection<OutfitRenterDate> OutfitRenterDates { get; set; }



    }
}
