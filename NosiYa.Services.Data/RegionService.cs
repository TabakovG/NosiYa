﻿namespace NosiYa.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using NosiYa.Data;
    using Interfaces;
    using Web.ViewModels.Region;
	using NosiYa.Web.ViewModels.OutfitSet;

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

        public async Task<IEnumerable<RegionAllViewModel>> AllAvailableRegionsAsync(AllRegionsPaginatedModel model)
        {
			return await this.context
				.Regions
				.AsNoTracking()
				.Where(r => r.IsActive)
				.Include(r=>r.Images)
				.Select(r => new RegionAllViewModel()
				{
					Id = r.Id,
					Name = r.Name,
                    ImageUrl = r.Images
	                    .Where(i=>i.IsDefault)
	                    .Select(i=>i.Url)
	                    .FirstOrDefault() ?? string.Empty
				})
				.ToArrayAsync();
		}

        public async Task<bool> ExistsByIdAsync(int id)
        {
	        return await this.context
		        .Regions
		        .AsNoTracking()
		        .AnyAsync(r => r.Id == id && r.IsActive);
        }

        public async Task<RegionDetailsViewModel> GetDetailsByIdAsync(int id)
        {
	        var region = await this.context
		        .Regions
		        .AsNoTracking()
		        .Include(r=>r.Images)
		        .Where(r => r.IsActive)
		        .FirstAsync(r => r.Id == id);

	        var model = new RegionDetailsViewModel
	        {
		        Id = region.Id,
		        Name = region.Name,
		        Description = region.Description,
		        Images = region
			        .Images
			        .Select(i => i.Url)
			        .ToArray()
	        };
            return model;
        }
    }
}
