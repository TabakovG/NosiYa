using Microsoft.EntityFrameworkCore;
using NosiYa.Data;
using NosiYa.Services.Data.Interfaces;

namespace NosiYa.Services.Data
{
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
    }
}
