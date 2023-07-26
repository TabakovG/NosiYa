namespace NosiYa.Web.ViewModels.Image
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstants.Image;

    public class ImageFormModel
    {
        [Required]
        [StringLength(ImageUrlMaxLength, MinimumLength = ImageUrlMinLength)]
        public string Url { get; set; } = null!;
        public bool IsDefault { get; set; } = false;

        public int? OutfitSetId { get; set; }
        public int? OutfitPartId { get; set; }
        public int? RegionId { get; set; }
        public int? EventId { get; set; }
    }
}
