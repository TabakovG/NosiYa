using System.ComponentModel.DataAnnotations.Schema;
using NosiYa.Data.Models.Outfit;

namespace NosiYa.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static NosiYa.Common.EntityValidationConstants.Image;

    public class Image
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ImageUrlMaxLength)]
        public string Url { get; set; } = null!;

        [ForeignKey(nameof(OutfitSet))]
        public int? OutfitSetId { get; set; }
        public OutfitSet? OutfitSet { get; set; }

        [ForeignKey(nameof(OutfitPart))]
        public int? OutfitPartId { get; set; }
        public OutfitPart? OutfitPart { get; set; }

        [ForeignKey(nameof(Region))]
        public int? RegionId { get; set; }
        public Region? Region { get; set; }

        [ForeignKey(nameof(Event))]
        public int? EventId { get; set; }
        public Event? Event { get; set; }
    }
}
