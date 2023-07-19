namespace NosiYa.Web.ViewModels.OutfitPart
{
    using System.ComponentModel.DataAnnotations;
    using Data.Models.Enums;

    using Image;
    using static Common.EntityValidationConstants.Outfit;

    public  class OutfitPartFormModel
    {
        public OutfitPartFormModel()
        {
            this.PartImages = new HashSet<ImageFormModel>();
        }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;

        [MaxLength(DescriptionMaxLength)]
        public string? Description { get; set; }

        [Required]
        [StringLength(ColorMaxLength, MinimumLength = ColorMinLength)]
        public string Color { get; set; } = null!;

        [Required]
        public RenterType RenterType { get; set; }

        [Required]
        public OutfitPartType OutfitPartType { get; set;}

        public int OutfitSetId { get; set; }

        public Guid OwnerId { get; set; }

        [Required]
        [StringLength(OwnerEmailMaxLength, MinimumLength= OwnerEmailMinLength)]
		public string OwnerEmail { get; set; }

        [Required]
        [StringLength(SizeMaxLength, MinimumLength = SizeMinLength)]
        public string Size { get; set; } = null!;

        public ICollection<ImageFormModel> PartImages { get; set; }


    }
}
