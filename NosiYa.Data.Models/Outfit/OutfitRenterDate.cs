namespace NosiYa.Data.Models.Outfit
{

    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class OutfitRenterDate
    {
	    public Guid OrderId { get; set; }

		[ForeignKey(nameof(Outfit))]
        public int OutfitId { get; set; }
        public OutfitSet Outfit { get; set; } = null!;

        [ForeignKey(nameof(Renter))]
        public Guid RenterId { get; set; }
        public ApplicationUser Renter { get; set; } = null!;

        [Required]
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime DateRangeStart { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime DateRangeEnd { get; set; }

		public bool IsActive { get; set; } = true;
        public bool IsApproved { get; set; } = false;


	}
}
