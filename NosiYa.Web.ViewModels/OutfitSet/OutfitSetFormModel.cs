namespace NosiYa.Web.ViewModels.OutfitSet
{
    using System.ComponentModel.DataAnnotations;
    
    using NosiYa.Data.Models.Enums;
    using Image;
    using OutfitPart;
    using static Common.EntityValidationConstants.Outfit;

    public class OutfitSetFormModel
    {
        public OutfitSetFormModel()
        {
            this.SetImages = new HashSet<ImageFormModel>();
            this.OutfitParts = new HashSet<OutfitPartFormModel>();
            this.IsActive = true;
            this.IsAvailable = true;
        }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;

        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        public int? RegionId { get; set; }

        public decimal PricePerDay { get; set; }

        [Required]
        [StringLength(ColorMaxLength, MinimumLength = ColorMinLength)]
        public string Color { get; set; } = null!;

        [Range(0,4)]
        public RenterType RenterType { get; set; }

        public bool IsAvailable { get; set; }

        public bool IsActive { get; set; }

        [Required]
        [StringLength(SizeMaxLength, MinimumLength = SizeMinLength)]
        public string Size { get; set; } = null!;

        public ICollection<ImageFormModel> SetImages { get; set; }
        public ICollection<OutfitPartFormModel> OutfitParts { get; set; }

    }
}
