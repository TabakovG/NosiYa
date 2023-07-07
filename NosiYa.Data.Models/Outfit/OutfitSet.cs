using System.ComponentModel.DataAnnotations.Schema;

namespace NosiYa.Data.Models.Outfit
{
    public class OutfitSet : OutfitBase
    {
        public OutfitSet() :base()
        {
            this.Accessories = new HashSet<Accessory>();
        }

        [ForeignKey(nameof(Shirt))]
        public int ShirtId { get; set; } 
        public Shirt Shirt { get; set; } = null!;

        [ForeignKey(nameof(Legs))]
        public int LegsId { get; set; }
        public Legs Legs { get; set; } = null!;

        [ForeignKey(nameof(Vest))]
        public int? VestId { get; set; }
        public Vest? Vest { get; set; }

        [ForeignKey(nameof(Belt))]
        public int? BeltId { get; set; }
        public Belt? Belt { get; set; }

        public ICollection<Accessory> Accessories { get; set; }


    }
}
