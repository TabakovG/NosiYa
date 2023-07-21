namespace NosiYa.Services.Data.Model
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
