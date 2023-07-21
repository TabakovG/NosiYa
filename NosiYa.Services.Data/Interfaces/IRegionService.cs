namespace NosiYa.Services.Data.Interfaces
{
	using Web.ViewModels.Region;
	public interface IRegionService
    {
        //Create:

        Task<int> CreateAndReturnIdAsync(RegionFormModel model);

		//Read:

		Task<IEnumerable<string>> GetAllRegionsNamesAsync(); //for outfitSet Query
        Task<IEnumerable<PossibleRegionsFormModel>>  GetAllRegionsAsync(); //for create / edit

        Task<IEnumerable<RegionAllViewModel>> AllAvailableRegionsAsync(AllRegionsPaginatedModel model); // for All view

		Task<bool> ExistsByIdAsync(int id);

		Task<RegionDetailsViewModel> GetDetailsByIdAsync(int id);

        //Update:



        //Delete:



    }
}
