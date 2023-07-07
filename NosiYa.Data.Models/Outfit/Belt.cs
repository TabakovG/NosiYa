using System.ComponentModel.DataAnnotations;

namespace NosiYa.Data.Models.Outfit
{

    using System.Drawing;

    public class Belt : OutfitBase
    {
        
        public double LengthInCm { get; set; }
        public double WidthInCm { get; set; }

    }
}
