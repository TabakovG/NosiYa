namespace NosiYa.Services.Data.Interfaces
{
	using Web.ViewModels.Region;
	public interface IRegionService
    {
        Task<IEnumerable<string>> GetAllRegionsNamesAsync(); //for outfitSet Query
        Task<IEnumerable<PossibleRegionsFormModel>>  GetAllRegionsAsync(); //for create / edit

        Task<IEnumerable<RegionAllViewModel>> AllAvailableRegionsAsync(AllRegionsPaginatedModel model); // for All view

		Task<bool> RegionExistsByIdAsync(int id); 

    }
}
