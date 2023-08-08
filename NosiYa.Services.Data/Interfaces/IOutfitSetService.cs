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
        Task<AllOutfitsFilteredAndPagedServiceModel> AllFilteredOutfitSetsByAvailabilityAsync(AllOutfitsQueryModel queryModel, bool AreAvailable);
        Task<IEnumerable<OutfitSetForOptionsViewModel>> GetAllOutfitSetsForOptionsAsync(); //parrent options for part
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
