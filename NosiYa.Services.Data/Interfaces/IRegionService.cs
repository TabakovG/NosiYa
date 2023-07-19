namespace NosiYa.Services.Data.Interfaces
{
    using Web.ViewModels.Region;

    public interface IRegionService
    {
        Task<IEnumerable<string>> GetAllRegionsNamesAsync();
        Task<IEnumerable<PossibleRegionsFormModel>>  GetAllRegionsAsync();
        Task<bool> RegionExistsByIdAsync(int id);

    }
}
