namespace NosiYa.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using NosiYa.Data;
    using Interfaces;
    using Web.ViewModels.Region;

    public class RegionService : IRegionService
    {
        private readonly NosiYaDbContext context;

        public RegionService(NosiYaDbContext _context)
        {
            this.context = _context;
        }

        public async Task<IEnumerable<string>> GetAllRegionsNamesAsync()
        {
            return await this.context
                .Regions
                .AsNoTracking()
                .Select(r => r.Name)
                .ToArrayAsync();
        }

        public async Task<IEnumerable<PossibleRegionsOnCreateOutfitSetFormModel>> GetAllRegionsAsync()
        {
            return await this.context
                .Regions
                .AsNoTracking()
                .Select(r => new PossibleRegionsOnCreateOutfitSetFormModel()
                {
                    Id = r.Id,
                    Name = r.Name
                })
                .ToArrayAsync();
        }
    }
}
