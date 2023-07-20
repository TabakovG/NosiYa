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

        //Delete:  --------------//---------------
        public async Task<OutfitPartForDelete> GetOutfitPartForDeleteAsync(int id)
        {
	        OutfitPart outfitPart = await this.context
		        .OutfitParts
		        .FirstAsync(o => o.Id == id && o.IsActive);

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

        public async Task DeleteByIdAsync(int outfitPartId)
        {
	        var outfitPartForDelete = await this.context
		        .OutfitParts
		        .FirstAsync(o => o.IsActive && o.Id == outfitPartId);

	        outfitPartForDelete.IsActive = false;

	        await this.context.SaveChangesAsync();
        }
	}
}
