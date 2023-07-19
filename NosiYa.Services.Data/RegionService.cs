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
                .Where(r=>r.IsActive)
                .Select(r => r.Name)
                .ToArrayAsync();
        }

        public async Task<IEnumerable<PossibleRegionsFormModel>> GetAllRegionsAsync()
        {
            return await this.context
                .Regions
                .AsNoTracking()
                .Where(r => r.IsActive)
                .Select(r => new PossibleRegionsFormModel()
                {
                    Id = r.Id,
                    Name = r.Name
                })
                .ToArrayAsync();
        }

        public async Task<bool> RegionExistsByIdAsync(int id)
        {
	        return await this.context
		        .Regions
		        .AsNoTracking()
		        .AnyAsync(r => r.Id == id && r.IsActive);
        }
    }
}
