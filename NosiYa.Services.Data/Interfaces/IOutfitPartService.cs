namespace NosiYa.Services.Data.Interfaces
{
    using Web.ViewModels.OutfitPart;

    public interface IOutfitPartService
    {
        Task<int> CreateAndReturnIdAsync(OutfitPartFormModel formModel);
        Task<bool> ExistByIdAsync(int outfitPartId);

        //Read
        Task<ICollection<OutfitPartViewModel>> GetAllPartsBySetIdAsync(int id);
        Task<OutfitPartDetailsViewModel> GetDetailsByIdAsync(int id);

        //Update

        Task<OutfitPartFormModel> GetForEditByIdAsync(int id);
        Task EditByIdAsync(int id, OutfitPartFormModel model);

		//Delete
		Task<OutfitPartForDelete> GetOutfitPartForDeleteAsync(int outfitPartId);
        Task<int> DeleteByIdAsyncAndReturnParentId(int outfitPartId);

        Task DeleteByOutfitSetIdAsync(int setId);
    }
}
