namespace NosiYa.Data.Models
{
    public class Region
    {
        public Region()
        {
            this.Outfits = new HashSet<Outfit.Outfit>();
        }

        public int RegionId { get; set;}

        public string RegionName { get; set; } = null!; 

        public ICollection<Outfit.Outfit> Outfits { get; set;}
    }
}
