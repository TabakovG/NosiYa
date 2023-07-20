using NosiYa.Services.Data.Model;
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
        Task<bool> ExistByIdAsync(int outfitId);
        
        //Update
        Task<OutfitSetDetailsViewModel> GetDetailsByIdAsync(int id);
        Task<OutfitSetFormModel> GetForEditByIdAsync(int id);
        Task EditByIdAsync(int outfitId, OutfitSetFormModel model);


        //Delete
        Task<OutfitSetForDelete> GetOutfitSetForDeleteAsync();
        Task DeleteByIdAsync(int outfitId);
    }
}
