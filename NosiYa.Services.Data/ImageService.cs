using Microsoft.EntityFrameworkCore;
using NosiYa.Data;
using NosiYa.Data.Models;

namespace NosiYa.Services.Data
{
	using Interfaces;
	using Web.ViewModels.Image;

	public class ImageService : IImageService
	{
		private readonly NosiYaDbContext context;

		public ImageService(NosiYaDbContext _context)
		{
			this.context = _context;
		}

		public async Task AddImageAsync(ImageFormModel model, Guid userId)
		{
			var image = new Image
			{
				Url = model.Url,
				OutfitSetId = model.OutfitSetId,
				IsDefault = model.IsDefault,
				OutfitPartId = model.OutfitPartId,
				RegionId = model.RegionId,
				EventId = model.EventId
			};

			await this.context.Images.AddAsync(image);
			await this.context.SaveChangesAsync();
		}

		public Task<bool> ImageExistByIdAsync(int id)
		{
			throw new NotImplementedException();
		}

		public async Task SetDefaultImageAsync(int relatedEntityId, string entity, int? imageId)
		{
			ICollection<Image> images = new HashSet<Image>();
			switch (entity)
			{
				case "event":
					images = await this.context.Images.Where(i => i.EventId == relatedEntityId).ToListAsync();
					break;
				case "set":
					images = await this.context.Images.Where(i => i.OutfitSetId == relatedEntityId).ToListAsync();
					break;
				case "region":
					images = await this.context.Images.Where(i => i.RegionId == relatedEntityId).ToListAsync();
					break;
				case "part":
					images = await this.context.Images.Where(i => i.OutfitPartId == relatedEntityId).ToListAsync();
					break;
			}

			imageId = imageId ?? images.First().Id;

			foreach (var image in images)
			{
				image.IsDefault = image.Id == imageId;
			}

			await this.context.SaveChangesAsync();
		}

		public Task DeleteImageByIdAsync(int id)
		{
			throw new NotImplementedException();
		}
	}
}
