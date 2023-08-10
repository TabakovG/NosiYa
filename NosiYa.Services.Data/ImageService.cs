namespace NosiYa.Services.Data
{
	using System.IO;

	using Microsoft.EntityFrameworkCore;

	using Interfaces;
	using Web.ViewModels.Image;
	using Common;
	using NosiYa.Data;
	using NosiYa.Data.Models;

	//using SixLabors.ImageSharp;
	using SixLabors.ImageSharp.Processing;
	using SixLabors.ImageSharp.Formats;
	using SixLabors.ImageSharp.Formats.Jpeg;
	using System;
	using System.Net;

	public class ImageService : IImageService
	{
		private readonly NosiYaDbContext context;

		public ImageService(NosiYaDbContext _context)
		{
			this.context = _context;
		}

		public async Task<int> AddImageAndReturnIdAsync(ImageFormModel model, Guid userId)
		{
			var image = new Image
			{
				Url = WebUtility.HtmlEncode(model.Url),
				OutfitSetId = model.OutfitSetId,
				IsDefault = model.IsDefault,
				OutfitPartId = model.OutfitPartId,
				RegionId = model.RegionId,
				EventId = model.EventId
			};

			await this.context.Images.AddAsync(image);
			await this.context.SaveChangesAsync();

			return image.Id;
		}

		public async Task<ICollection<ImageViewModel>> GetRelatedImagesAsync(int relatedEntityId, string entity)
		{
			ICollection<Image> images = new HashSet<Image>();
			switch (entity)
			{
				case EntityTypesConst.Event:
					images = await this.context.Images.Where(i => i.EventId == relatedEntityId).ToListAsync();
					break;
				case EntityTypesConst.OutfitSet:
					images = await this.context.Images.Where(i => i.OutfitSetId == relatedEntityId).ToListAsync();
					break;
				case EntityTypesConst.Region:
					images = await this.context.Images.Where(i => i.RegionId == relatedEntityId).ToListAsync();
					break;
				case EntityTypesConst.OutfitPart:
					images = await this.context.Images.Where(i => i.OutfitPartId == relatedEntityId).ToListAsync();
					break;
			}

			return images.Select(i => new ImageViewModel()
			{
				Id = i.Id,
				ImageUrl = i.Url
			})
				.ToList();
		}

		public async Task<string?> GetDefaultIfExistsAsync(int relatedEntityId, string entity)
		{
			var result = this.context
				.Images
				.AsNoTracking()
				.Where(i => i.IsDefault)
				.AsQueryable();

			switch (entity.ToLower())
			{
				case EntityTypesConst.Event:
					result = result.Where(i => i.EventId == relatedEntityId);
					break;
				case EntityTypesConst.OutfitSet:
					result = result.Where(i => i.OutfitSetId == relatedEntityId);
					break;
				case EntityTypesConst.Region:
					result = result.Where(i => i.RegionId == relatedEntityId);
					break;
				case EntityTypesConst.OutfitPart:
					result = result.Where(i => i.OutfitPartId == relatedEntityId);
					break;
			}

			return await result.Select(i=>i.Url).FirstOrDefaultAsync();
		}


		public async Task<bool> ImageExistByIdAsync(int id)
		{
			return await this.context
				.Images
				.AnyAsync(i => i.Id == id);
		}

		public async Task SetDefaultImageAsync(int relatedEntityId, string entity, int? imageId = null)
		{
			ICollection<Image> images = new HashSet<Image>();

			switch (entity.ToLower())
			{
				case EntityTypesConst.Event:
					images = await this.context.Images.Where(i => i.EventId == relatedEntityId).ToListAsync();
					break;
				case EntityTypesConst.OutfitSet:
					images = await this.context.Images.Where(i => i.OutfitSetId == relatedEntityId).ToListAsync();
					break;
				case EntityTypesConst.Region:
					images = await this.context.Images.Where(i => i.RegionId == relatedEntityId).ToListAsync();
					break;
				case EntityTypesConst.OutfitPart:
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

		public async Task DeleteImageByIdAsync(int id, string root)
		{
			var image = await this.context
				.Images
				.Where(i => i.Id == id)
				.FirstAsync();

			this.context.Images.Remove(image);

			if (File.Exists(root + image.Url))
			{
				File.Delete(root + image.Url);
			}

			await this.context.SaveChangesAsync();
		}

		public async Task DeleteRelatedImagesByParentIdAsync(int relatedEntityId, string entity, string root)
		{
			var images = this.context
				.Images
				.AsNoTracking()
				.AsQueryable();

			switch (entity)
			{
				case EntityTypesConst.Event:
					images = images.Where(i => i.EventId == relatedEntityId);
					break;
				case EntityTypesConst.Region:
					images = images.Where(i => i.RegionId == relatedEntityId);
					break;
				case EntityTypesConst.OutfitPart:
					images = images.Where(i => i.OutfitPartId == relatedEntityId);
					break;
				case EntityTypesConst.OutfitSet:
					images = images.Where(i => i.OutfitSetId == relatedEntityId);
					break;
			}

			foreach (var image in images)
			{
				if (File.Exists(root + image.Url))
				{
					File.Delete(root + image.Url);
				}
			}
			this.context.Images.RemoveRange(await images.ToArrayAsync());

			await this.context.SaveChangesAsync();
		}
	}
}
