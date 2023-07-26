using NosiYa.Data.Models.Enums;
using NosiYa.Services.Data.Models;
using NosiYa.Web.ViewModels.OutfitSet;

namespace NosiYa.Services.Data.Interfaces
{

    public interface IOutfitSetService
    {
        //Create
        Task<int> CreateOutfitSetAndReturnIdAsync(OutfitSetFormModel formModel);

        //Read
        Task<AllOutfitsFilteredAndPagedServiceModel> AllAvailableOutfitSetsAsync(AllOutfitsQueryModel queryModel);
        Task<IEnumerable<OutfitSetAllViewModel>> AllOutfitSetsByUserIdAsync(string userId);
        Task<IEnumerable<OutfitSetForOptionsViewModel>> GetAllOutfitSetsForOptionsAsync();
        Task<bool> ExistByIdAsync(int outfitId);
        Task<RenterType> GetRenterTypeByIdAsync(int id);
        Task<OutfitSetForRentViewModel> GetForRentByIdAsync(int id);

		//Update
		Task<OutfitSetDetailsViewModel> GetDetailsByIdAsync(int id);
        Task<OutfitSetFormModel> GetForEditByIdAsync(int id);
        Task EditByIdAsync(int outfitId, OutfitSetFormModel model);


        //Delete
        Task<OutfitSetForDelete> GetOutfitSetForDeleteAsync(int id);
        Task DeleteByIdAsync(int outfitId);
    }
}
