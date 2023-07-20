namespace NosiYa.Services.Data.Interfaces
{
    using Web.ViewModels.OutfitPart;

    public interface IOutfitPartService
    {
        Task<int> CreateAndReturnIdAsync(OutfitPartFormModel formModel);
        Task<bool> ExistByIdAsync(int outfitPartId);


        //Delete
        Task<OutfitPartForDelete> GetOutfitPartForDeleteAsync(int outfitPartId);
        Task<int> DeleteByIdAsyncAndReturnParentId(int outfitPartId);
	}
}
