namespace NosiYa.Services.Data.Interfaces
{
    public interface IRegionService
    {
        Task<IEnumerable<string>> GetAllRegionsNamesAsync();
    }
}
