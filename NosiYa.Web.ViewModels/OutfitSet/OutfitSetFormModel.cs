namespace NosiYa.Web.ViewModels.OutfitSet
{
    using System.ComponentModel.DataAnnotations;
    
    using NosiYa.Data.Models.Enums;
    using Image;
    using OutfitPart;
    using Region;
    using static Common.EntityValidationConstants.Outfit;

    public class OutfitSetFormModel
    {
        public OutfitSetFormModel()
        {
            this.Images = new HashSet<ImageViewModel>();
            this.OutfitParts = new HashSet<OutfitPartFormModel>();
            this.Regions = new HashSet<PossibleRegionsFormModel>();
            this.IsActive = true;
        }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;

        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        public int RegionId { get; set; }
        public IEnumerable<PossibleRegionsFormModel> Regions { get; set; }

        public decimal PricePerDay { get; set; }

        [Required]
        [StringLength(ColorMaxLength, MinimumLength = ColorMinLength)]
        public string Color { get; set; } = null!;

        [Range(RenterTypeMinValue,RenterTypeMaxValue)]
        public RenterType RenterType { get; set; }

        public bool IsActive { get; set; }

        [Required]
        [StringLength(SizeMaxLength, MinimumLength = SizeMinLength)]
        public string Size { get; set; } = null!;

        public ICollection<ImageViewModel> Images { get; set; }
        public ICollection<OutfitPartFormModel> OutfitParts { get; set; } //TODO not needed ?

    }
}
