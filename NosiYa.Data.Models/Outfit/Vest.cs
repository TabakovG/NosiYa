namespace NosiYa.Data.Models.Outfit
{
    using System.Drawing;

    public class Vest
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public Region? Region { get; set; }

        public string? Description { get; set; }

        public double Length { get; set; }
        public double ShouldersWidth { get; set; }
        public double Sleeve { get; set; }

        public Color Color { get; set; }

        public string Picture { get; set; } = null!;

        public UserType UserType { get; set; }

        public int? OutfitId { get; set; }
        public Outfit? Outfit { get; set; }
    }
}
