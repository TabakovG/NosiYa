using NosiYa.Data.Models.Enums;
using NosiYa.Data.Models.Outfit;

namespace NosiYa.Services.Data
{
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
        
        public Task<int> CreateAndReturnIdAsync(OutfitPartFormModel formModel)
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
            }
        }
    }
}
