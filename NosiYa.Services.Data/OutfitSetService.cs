namespace NosiYa.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using NosiYa.Data;
    using NosiYa.Data.Models.Outfit;
    using NosiYa.Data.Models.Enums;

    using Interfaces;
    using Models;
    using Web.ViewModels.OutfitSet;
    using Web.ViewModels.OutfitSet.Enums;
    using static Common.ApplicationConstants;

    public class OutfitSetService : IOutfitSetService
    {
        private readonly NosiYaDbContext context;

        public OutfitSetService(NosiYaDbContext _context)
        {
            this.context = _context;
        }

        //Create: --------------//---------------

        public async Task<int> CreateOutfitSetAndReturnIdAsync(OutfitSetFormModel formModel)
        {
            OutfitSet outfit = new OutfitSet
            {
                Name = formModel.Name,
                Description = formModel.Description,
                RegionId = formModel.RegionId,
                PricePerDay = formModel.PricePerDay, //TODO decimal parse ?
                Color = formModel.Color,
                RenterType = formModel.RenterType,
                IsAvailable = formModel.IsAvailable,
                IsActive = formModel.IsActive,
                Size = formModel.Size,
                /*
                Images = formModel.Images, //TODO foreach fm create and add image
                OutfitParts = formModel.OutfitParts, //TODO foreach fm create and add part*/
            };

            await this.context.OutfitSets.AddAsync(outfit);
            await this.context.SaveChangesAsync();

            return outfit.Id;
        }

        //Read:  --------------//---------------

        public async Task<AllOutfitsFilteredAndPagedServiceModel> AllAvailableOutfitSetsAsync(AllOutfitsQueryModel queryModel)
        {
            IQueryable<OutfitSet> outfitQuery = this.context
                .OutfitSets
                .Where(o => o.IsAvailable && o.IsActive)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryModel.SearchTerm))
            {
                string wildCard = $"%{queryModel.SearchTerm.ToLower()}%";

                outfitQuery = outfitQuery
                    .Where(o => EF.Functions.Like(o.Name, wildCard) ||
                                EF.Functions.Like(o.Description ?? string.Empty, wildCard) ||  //to be tested
                                EF.Functions.Like(o.Color, wildCard));
            }

            if (!string.IsNullOrWhiteSpace(queryModel.Region))
            {
                outfitQuery = outfitQuery
                    .Where(o => o.Region != null && o.Region.Name == queryModel.Region);
            }

            if (!string.IsNullOrWhiteSpace(queryModel.Size.ToString())
                && queryModel.Size != SizeOptions.All)
            {
                outfitQuery = outfitQuery
                    .Where(o => o.Size.Contains($"-{queryModel.Size.ToString()}-"));
            }

            if (!string.IsNullOrWhiteSpace(queryModel.RenterType.ToString())
                && queryModel.RenterType != RenterType.All)
            {
                outfitQuery = outfitQuery
                    .Where(o => o.RenterType == queryModel.RenterType);
            }


	            outfitQuery = queryModel.OutfitSorting switch
	            {
	                OutfitsSorting.Newest => outfitQuery
	                    .OrderByDescending(o => o.CreatedOn),
	                OutfitsSorting.Oldest => outfitQuery
	                    .OrderBy(o => o.CreatedOn),
	                OutfitsSorting.PriceAscending => outfitQuery
	                    .OrderBy(o => o.PricePerDay),
	                OutfitsSorting.PriceDescending => outfitQuery
	                    .OrderByDescending(h => h.PricePerDay),
	                OutfitsSorting.PopularityDescending => outfitQuery
	                    .OrderByDescending(o => o.OutfitRenterDates.Count()),
	                _ => outfitQuery
	                    .OrderBy(o => o.Name)
	                    .ThenByDescending(h => h.CreatedOn)
	            };

            IEnumerable<OutfitSetAllViewModel> allOutfits = await outfitQuery
                .Where(o => o.IsActive)
                .Skip((queryModel.CurrentPage - 1) * queryModel.OutfitsPerPage)
                .Take(queryModel.OutfitsPerPage)
                .Select(o => new OutfitSetAllViewModel
                {
                    Id = o.Id,
                    Name = o.Name,
                    ImageUrl = o.Images
	                    .FirstOrDefault(i => i.IsDefault) != null 
	                    ? o.Images.FirstOrDefault(i => i.IsDefault)!.Url 
	                    : DefaultImagePath
                })
                .ToArrayAsync();

            int outfitsCount = outfitQuery.Count();

            return new AllOutfitsFilteredAndPagedServiceModel()
            {
                TotalOutfits = outfitsCount,
                OutfitSets = allOutfits
            };

        }

        public async Task<IEnumerable<OutfitSetForOptionsViewModel>> GetAllOutfitSetsForOptionsAsync()
        {
			return await this.context
				.OutfitSets
				.AsNoTracking()
				.Where(o => o.IsActive)
				.Select(o => new OutfitSetForOptionsViewModel()
				{
					Id = o.Id,
					Name = o.Name
				})
				.ToArrayAsync();
		}

        public async Task<bool> ExistByIdAsync(int outfitId)
        {
	        return await this.context
		        .OutfitSets
		        .AnyAsync(o => o.IsActive && o.Id == outfitId);
        }

        public async Task<RenterType> GetRenterTypeByIdAsync(int id)
        {
			var outfitSet = await this.context
				.OutfitSets
				.FirstAsync(o => o.Id == id); //TODO only admin to be able to see non active

			return outfitSet.RenterType;
        }

        public async Task<OutfitSetForRentViewModel> GetForRentByIdAsync(int id)
        {

	        var outfitSet = await this.context
				.OutfitSets
				.AsNoTracking()
				.Include(r => r.Region)
				.Include(i => i.Images)
				.FirstAsync(o => o.Id == id);


			return new OutfitSetForRentViewModel
			{
				Name = outfitSet.Name,
				Description = outfitSet.Description ?? string.Empty,
				Size = outfitSet.Size,
				RenterType = outfitSet.RenterType.ToString(),
				Images = outfitSet.Images.Select(i => i.Url).ToList()
			};
        }

        public async Task<OutfitSetDetailsViewModel> GetDetailsByIdAsync(int id)
        {
	        var outfitSet = await this.context
		        .OutfitSets
		        .AsNoTracking()
                .Include(r=>r.Region)
		        .Include(i=>i.Images)
		        .FirstAsync(o => o.Id == id); //TODO only admin to be able to see non active

	            var outfitModel = new OutfitSetDetailsViewModel
	        {
		        Id = outfitSet.Id,
		        Name = outfitSet.Name,
		        Description = outfitSet.Description,
		        Region = outfitSet.Region.Name,
		        PricePerDay = outfitSet.PricePerDay,
		        RenterType = outfitSet.RenterType.ToString(),
		        IsAvailable = outfitSet.IsAvailable,
		        Size = outfitSet.Size,
                Images = outfitSet.Images.Select(i=>i.Url).ToArray()
            };

            return outfitModel;
        }

        //Update:  --------------//---------------

        public async Task<OutfitSetFormModel> GetForEditByIdAsync(int id)
        {
            var outfitSet = await this.context
                .OutfitSets
                .Include(r => r.Region)
                .FirstAsync(o => o.Id == id);

            var editModel = new OutfitSetFormModel
            {
                Name = outfitSet.Name,
                Description = outfitSet.Description ?? string.Empty,
                RegionId = outfitSet.RegionId,
                PricePerDay = outfitSet.PricePerDay,
                Color = outfitSet.Color,
                RenterType = outfitSet.RenterType,
                IsAvailable = outfitSet.IsAvailable,
                Size = outfitSet.Size,
            };

            return editModel;
        }

        public async Task EditByIdAsync(int outfitId, OutfitSetFormModel model)
        {
	        var outfit = await this.context
		        .OutfitSets
		        .FindAsync(outfitId);

            outfit!.Name = model.Name;
            outfit.Description = model.Description;
            outfit.RegionId = model.RegionId;
            outfit.PricePerDay = model.PricePerDay;
            outfit.Color = model.Color;
            outfit.RenterType = model.RenterType;
            outfit.IsAvailable = model.IsAvailable;
            outfit.Size = model.Size;

            await this.context.SaveChangesAsync();
        }


        //Delete:  --------------//---------------
        public async Task<OutfitSetForDelete> GetOutfitSetForDeleteAsync(int id)
        {
			OutfitSet outfit = await this.context
				.OutfitSets
				.FirstAsync(o=>o.Id == id && o.IsActive);

			return new OutfitSetForDelete
			{
				Name = outfit.Name,
				Description = outfit.Description!,
                Images = outfit.Images.Select(i=>i.Url).ToArray()
			};
		}

        public async Task DeleteByIdAsync(int outfitId)
        {
	        var outfitForDelete = await this.context
		        .OutfitSets
		        .FirstAsync(o => o.IsActive && o.Id == outfitId);

	        outfitForDelete.IsActive = false;

            await this.context.SaveChangesAsync();
        }
    }
}
