namespace NosiYa.Data.Models.Outfit
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class OutfitForCart
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Outfit))]
        public int OutfitId { get; set; }

        public OutfitSet Outfit { get; set; } = null!;

        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }

        [Required]
        [ForeignKey(nameof(Cart))]
        public int CartId { get; set; }
        public Cart Cart { get; set; } = null!;
    }
}
