namespace NosiYa.Data.Models.Outfit
{

    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Outfit;

    public class OutfitRenterDate
    {
        [ForeignKey(nameof(Outfit))]
        public int OutfitId { get; set; }
        public OutfitSet Outfit { get; set; } = null!;

        [ForeignKey(nameof(Renter))]
        public Guid RenterId { get; set; }
        public ApplicationUser Renter { get; set; } = null!;

        [Required]
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public bool IsActive { get; set; } = true;

    }
}
