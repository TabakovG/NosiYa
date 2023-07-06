
namespace NosiYa.Data.Models.Outfit
{

    using System.Drawing;

    public class Accessory
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public Color Color { get; set; }

        public string Picture { get; set; } = null!;

        public UserType UserType { get; set; }

        public int? OutfitId { get; set; }
        public Outfit? Outfit { get; set; }
    }
}
