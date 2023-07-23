using NosiYa.Data.Models.Enums;

namespace NosiYa.Services.Data
{
	using Microsoft.EntityFrameworkCore;

	using NosiYa.Data.Models.Outfit;
    using NosiYa.Data;
    using Interfaces;
    using Web.ViewModels.OutfitPart;

    public class OutfitPartService : IOutfitPartService
    {
        private readonly NosiYaDbContext context;

        public OutfitPartService(NosiYaDbContext _context)
        {
            this.context = _context;
        }

        //Create: --------------//---------------
        
        public async Task<int> CreateAndReturnIdAsync(OutfitPartFormModel formModel)
        {
            OutfitPart part = new OutfitPart
            {
                Name = formModel.Name,
                Description = formModel.Description,
                Color = formModel.Color,
                RenterType = formModel.RenterType,
                OutfitPartType = formModel.OutfitPartType,
                OutfitSetId = formModel.OutfitSetId,
                OwnerId = formModel.OwnerId,
                Size = formModel.Size,
                //Images = null TODO fill the images via the image service
            };

                await this.context.OutfitParts.AddAsync(part);
                await this.context.SaveChangesAsync();

                return part.Id;
        }

		//Read:   --------------//---------------

		public async Task<bool> ExistByIdAsync(int id)
		{
			return await this.context
				.OutfitParts
				.AnyAsync(o => o.IsActive && o.Id == id);
		}

        public async Task<ICollection<OutfitPartViewModel>> GetAllPartsBySetIdAsync(int setId)
        {
	        return await this.context
		        .OutfitParts
		        .AsNoTracking()
		        .Include(i=>i.Images)
		        .Where(p => p.IsActive && p.OutfitSetId == setId)
		        .Select(p => new OutfitPartViewModel
		        {
			        Id = p.Id,
			        Name = p.Name,
					imageUrl = p.Images
						.Where(i=>i.IsDefault)
						.Select(i=>i.Url)
						.FirstOrDefault() ?? string.Empty
					
		        })
		        .ToListAsync();
			
        }
		public async Task<OutfitPartDetailsViewModel> GetDetailsByIdAsync(int id)
        {
			var outfitPart = await this.context
				.OutfitParts
				.Include(p => p.OutfitSet)
				.Include(p => p.Images)
				.Include(p=>p.Owner)
				.FirstAsync(p => p.Id == id); //TODO only admin to be able to see non active

			var outfitPartModel = new OutfitPartDetailsViewModel
			{
				Id = outfitPart.Id,
				Name = outfitPart.Name,
				Description = outfitPart.Description,
				Color = outfitPart.Color,
				RenterType = outfitPart.RenterType.ToString(),
				OutfitPartType = outfitPart.OutfitPartType.ToString(),
				OutfitSet = outfitPart.OutfitSet.Name,
				OwnerEmail = outfitPart.Owner.Email,
				Size = outfitPart.Size,
				Images = outfitPart
					.Images
					.Select(i=>i.Url)
					.ToList()
			};

			return outfitPartModel;
		}

		//Update: --------------//---------------

		public async Task<OutfitPartFormModel> GetForEditByIdAsync(int id)
		{
			var outfitPart = await this.context
				.OutfitParts
				.Include(p=>p.Owner)
				.FirstAsync(p=>p.IsActive && p.Id == id);

			var editModel = new OutfitPartFormModel
			{
				Name = outfitPart.Name,
				Description = outfitPart.Description,
				Color = outfitPart.Color,
				RenterType = outfitPart.RenterType,
				OutfitPartType = outfitPart.OutfitPartType,
				OutfitSetId = outfitPart.OutfitSetId,
				OwnerId = outfitPart.OwnerId, //TODO do i need that
				OwnerEmail = outfitPart.Owner.Email,
				Size = outfitPart.Size,
			};

			return editModel;
		}

		public async Task EditByIdAsync(int id, OutfitPartFormModel model)
		{
			var outfitPart = await this.context
				.OutfitParts
				.FirstAsync(p => p.IsActive && p.Id == id);

			outfitPart.Name = model.Name;
			outfitPart.Description = model.Description;
			outfitPart.Color = model.Color;
			outfitPart.RenterType = model.RenterType;
			outfitPart.OutfitPartType = model.OutfitPartType;
			outfitPart.OwnerId = model.OwnerId;
			outfitPart.Size = model.Size;
			outfitPart.OutfitSetId = model.OutfitSetId;

			await this.context.SaveChangesAsync();
		}

		//Delete:  --------------//---------------

		public async Task<OutfitPartForDelete> GetOutfitPartForDeleteAsync(int outfitPartId)
        {
	        OutfitPart outfitPart = await this.context
		        .OutfitParts
		        .AsNoTracking()
		        .Include(o=>o.Owner)
		        .Include(os=>os.OutfitSet)
		        .Include(i=>i.Images)
		        .FirstAsync(o => o.Id == outfitPartId && o.IsActive);

	        return new OutfitPartForDelete
	        {
		        Name = outfitPart.Name,
		        Description = outfitPart.Description,
		        OutfitPartType = outfitPart.OutfitPartType.ToString(),
		        OwnerEmail = outfitPart.Owner.Email,
		        RenterType = outfitPart.RenterType.ToString(),
		        OutfitSetName = outfitPart.OutfitSet.Name,
		        Images = outfitPart.Images.Select(i=>i.Url).ToArray()
			};
        }

        public async Task<int> DeleteByIdAsyncAndReturnParentId(int outfitPartId)
        {
	        var outfitPartForDelete = await this.context
		        .OutfitParts
		        .FirstAsync(o => o.IsActive && o.Id == outfitPartId);

	        outfitPartForDelete.IsActive = false;

	        await this.context.SaveChangesAsync();

	        return outfitPartForDelete.OutfitSetId;
        }
	}
}
