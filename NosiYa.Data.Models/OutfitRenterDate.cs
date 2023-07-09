namespace NosiYa.Data.Models
{

    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Outfit;

    public class OutfitRenterDate
    {
        [ForeignKey(nameof(Outfit))]
        public int OutfitId { get; set; }
        public OutfitPart Outfit { get; set; } = null!;

        [ForeignKey(nameof(Renter))]
        public Guid RenterId { get; set; }
        public ApplicationUser Renter { get; set; } = null!;

        [Required]
        public DateTime Date { get; set; }
    }
}
