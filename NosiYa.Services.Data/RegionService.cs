namespace NosiYa.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using NosiYa.Data;
	using NosiYa.Data.Models;
	using Models;
    using Interfaces;
    using Web.ViewModels.Region;
    using System.Net;

    public class RegionService : IRegionService
    {
        private readonly NosiYaDbContext context;

        public RegionService(NosiYaDbContext _context)
        {
            this.context = _context;
        }

        public async Task<int> CreateAndReturnIdAsync(RegionFormModel model)
        {
	        var region = new Region
	        {
		        Name = WebUtility.HtmlEncode(model.Name),
		        Description = WebUtility.HtmlEncode(model.Description),
	        };

	        await this.context.Regions.AddAsync(region);
            await this.context.SaveChangesAsync();

            return region.Id;
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

        public async Task<AllRegionsPagedServiceModel> AllAvailableRegionsAsync(AllRegionsPaginatedModel model)
        {
	        var regions = this.context
		        .Regions
		        .AsNoTracking()
		        .Where(r => r.IsActive)
		        .AsQueryable();

			int regionsCount = regions.Count();

			var result =  await regions
				.Include(r=>r.Images)
				.OrderBy(r=>r.Name)
				.Skip((model.CurrentPage - 1) * model.RegionsPerPage)
				.Take(model.RegionsPerPage)
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

			return new AllRegionsPagedServiceModel()
			{
				RegionsCount = regionsCount,
				Regions = result
			};
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

        public async Task<RegionFormModel> GetForEditByIdAsync(int id)
        {
	        var region = await this.context
		        .Regions
		        .FirstAsync(r => r.IsActive && r.Id == id);

	        var model = new RegionFormModel
	        {
		        Name = region.Name,
		        Description = region.Description,
	        };

            return model;
        }

        public async Task EditByIdAsync(int id, RegionFormModel model)
        {
			var region = await this.context
				.Regions
				.FirstAsync(r => r.IsActive && r.Id == id);

			region.Name = WebUtility.HtmlEncode(model.Name);
			region.Description = WebUtility.HtmlEncode(model.Description);

			await this.context.SaveChangesAsync();
		}

        public async Task DeleteByIdAsync(int id)
        {
			var region = await this.context
				.Regions
				.FirstAsync(r => r.IsActive && r.Id == id);

			region.IsActive = false;

			await this.context.SaveChangesAsync();
        }
    }
}
