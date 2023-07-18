namespace NosiYa.Services.Data.Interfaces
{
    using Web.ViewModels.Region;

    public interface IRegionService
    {
        Task<IEnumerable<string>> GetAllRegionsNamesAsync();
        Task<IEnumerable<PossibleRegionsOnCreateOutfitSetFormModel>>  GetAllRegionsAsync();
    }
}
