using NosiYa.Data.Models.Enums;

namespace NosiYa.Web.ViewModels.OutfitSet
{
    public class OutfitSetFormModel
    {
        public OutfitSetFormModel()
        {
            this.Images = new HashSet<ImageFormModel>();
            this.OutfitParts = new HashSet<OutfitPartFormModel>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public int RegionId { get; set; }
        public decimal PricePerDay { get; set; }
        public string Color { get; set; }
        public RenterType RenterType { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsActive { get; set; }
        public string Size { get; set; }
        public ICollection<ImageFormModel> Images { get; set; }
        public ICollection<OutfitPartFormModel> OutfitParts { get; set; }

    }
}
