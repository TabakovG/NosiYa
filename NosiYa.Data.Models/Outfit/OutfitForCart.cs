namespace NosiYa.Data.Models.Outfit
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class OutfitForCart
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(OutfitSet))]
        public int OutfitId { get; set; }

        public OutfitSet OutfitSet { get; set; } = null!;

        [Required]
        [Column(TypeName = "date")]
		public DateTime FromDate { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime ToDate { get; set; }

        [Required]
        [ForeignKey(nameof(Cart))]
        public int CartId { get; set; }
        public Cart Cart { get; set; } = null!;

        public bool IsActive { get; set; } = true;
    }
}
