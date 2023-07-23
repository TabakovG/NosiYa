namespace NosiYa.Services.Data.Models
{
	using Web.ViewModels.OutfitSet;
    public class AllOutfitsFilteredAndPagedServiceModel
    {
        public AllOutfitsFilteredAndPagedServiceModel()
        {
            this.OutfitSets = new HashSet<OutfitSetAllViewModel>();
        }
        public int TotalOutfits { get; set; }
        public IEnumerable<OutfitSetAllViewModel> OutfitSets { get; set; }
    }
}
