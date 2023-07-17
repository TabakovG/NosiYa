using NosiYa.Data.Models.Enums;

namespace NosiYa.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using NosiYa.Data;
    using NosiYa.Data.Models.Outfit;
    using Interfaces;
    using Model;
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

        public Task<string> CreateOutfitSetAndReturnIdAsync(OutfitSetFormModel formModel)
        {
            throw new NotImplementedException();
        }

        //Read:  --------------//---------------
        public async Task<AllOutfitsFilteredAndPagedServiceModel> AllAvailableOutfitSetsAsync(AllOutfitsQueryModel queryModel)
        {
            IQueryable<OutfitSet> outfitQuery = this.context
                .OutfitSets
                .Where(o => o.IsAvailable == true)
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
                    .Where(o => o.Size.Contains(queryModel.Size.ToString()));
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
                    ImageUrl = o.Images.FirstOrDefault(i => i.IsDefault) != null ? o.Images.FirstOrDefault(i => i.IsDefault)!.Url : DefaultOutfitImagePath
                })
                .ToArrayAsync();

            int outfitsCount = outfitQuery.Count();

            return new AllOutfitsFilteredAndPagedServiceModel()
            {
                TotalOutfits = outfitsCount,
                OutfitSets = allOutfits
            };

        }

        public Task<IEnumerable<OutfitSetAllViewModel>> AllOutfitSetsByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistByIdAsync(int outfitId)
        {
            throw new NotImplementedException();
        }

        public Task<OutfitSetDetailsViewModel> GetDetailsByIdAsync()
        {
            throw new NotImplementedException();
        }

        //Update:  --------------//---------------

        public Task<OutfitSetFormModel> EditByIdAsync(int outfitId)
        {
            throw new NotImplementedException();
        }


        //Delete:  --------------//---------------
        public Task<OutfitSetForDelete> GetOutfitSetForDeleteAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(int outfitId)
        {
            throw new NotImplementedException();
        }
    }
}
