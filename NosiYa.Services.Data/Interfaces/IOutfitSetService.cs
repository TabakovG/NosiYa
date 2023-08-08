namespace NosiYa.Services.Data.Interfaces
{
	using NosiYa.Data.Models.Enums;
	using Models;
	using Web.ViewModels.OutfitSet;

    public interface IOutfitSetService
    {
        //Create
        Task<int> CreateOutfitSetAndReturnIdAsync(OutfitSetFormModel formModel);

        //Read
        Task<AllOutfitsFilteredAndPagedServiceModel> AllFilteredOutfitSetsByAvailabilityAsync(AllOutfitsQueryModel queryModel, bool areAvailable);
        Task<IEnumerable<OutfitSetForOptionsViewModel>> GetAllOutfitSetsForOptionsAsync(); //parrent options for part
        Task<bool> ExistByIdAsync(int outfitId);
        Task<RenterType> GetRenterTypeByIdAsync(int id);
        Task<OutfitSetForRentViewModel> GetForRentByIdAsync(int id);
        Task<bool> HasPartsByIdAsync(int id);
        Task<bool> HasFutureReservationsAsync(int id);

		//Update
		Task<OutfitSetDetailsViewModel> GetDetailsByIdAsync(int id);
        Task<OutfitSetFormModel> GetForEditByIdAsync(int id);
        Task EditByIdAsync(int outfitId, OutfitSetFormModel model);
        Task ActivateByIdAsync(int id);
        Task DeactivateByIdAsync(int id);


        //Delete
		Task<OutfitSetForDelete> GetOutfitSetForDeleteAsync(int id);
        Task DeleteByIdAsync(int outfitId);
    }
}
