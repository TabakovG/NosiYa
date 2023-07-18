namespace NosiYa.Services.Data.Interfaces
{
    using Web.ViewModels.OutfitPart;

    public interface IOutfitPartService
    {
        Task<int> CreateAndReturnIdAsync(OutfitPartFormModel formModel);
    }
}
