﻿namespace NosiYa.Web.Controllers
{
	using System.Collections.Generic;
	using Microsoft.AspNetCore.Mvc;

	using Infrastructure.Extensions;

	using NosiYa.Services.Data.Interfaces;
	using static Common.EntityTypesConst;
	using static Common.NotificationMessagesConstants;

	using ViewModels.Image;

	public class ImageController : Controller
	{
		private readonly IImageService imageService;
		private readonly IWebHostEnvironment hostingEnvironment;

		public ImageController(IImageService imageService, IWebHostEnvironment hostingEnvironment)
		{
			this.imageService = imageService;
			this.hostingEnvironment = hostingEnvironment;
		}

		public async Task AddImagesOnEntityCreateAsync(int entityId, string entityType, ICollection<IFormFile> elementImages)
		{

			await this.UploadImagesAsync(entityId, entityType, elementImages);
			await this.imageService.SetDefaultImageAsync(entityId, entityType);
		}

		public async Task UploadImagesAsync(int entityId, string entityType, ICollection<IFormFile> elementImages)
		{
			try
			{
				foreach (var imageFormFile in elementImages)
				{
					var imagePath = await UploadImage(imageFormFile, entityType);

					var imageModel = new ImageFormModel
					{
						Url = imagePath,
					};

					switch (entityType)
					{
						case OutfitSet:
							imageModel.OutfitSetId = entityId;
							break;
						case OutfitPart:
							imageModel.OutfitPartId = entityId;
							break;
						case Event:
							imageModel.EventId = entityId;
							break;
						case Region:
							imageModel.RegionId = entityId;
							break;
					}

					var userId = Guid.Parse(this.User!.GetId()!);
					
					//Create DB CIs and relations

					await this.imageService.AddImageAndReturnIdAsync(imageModel, userId);
					var hasDefault = await this.imageService.HasDefaultAsync(entityId, entityType);
					if (!hasDefault)
					{
						await this.imageService.SetDefaultImageAsync(entityId, entityType);
					}
				}
			}
			catch (Exception)
			{
				this.TempData[ErrorMessage] =
					"Unexpected error occurred during the images processing! Please try again later or contact administrator";
			}
		}


		[HttpPost]
		public async Task<IActionResult> Delete(int id, [FromForm] string returnUrl)
		{
			var imageExists = await this.imageService.ImageExistByIdAsync(id);

			if (!imageExists)
			{
				this.TempData[ErrorMessage] = "Снимка с този идентификатор не съществува!";

				return this.Redirect(returnUrl);
			}

			try
			{
				await this.imageService.DeleteImageByIdAsync(id, this.hostingEnvironment.WebRootPath);

				this.TempData[WarningMessage] = "Снимката беше изтрита успешно!";
				return this.Redirect(returnUrl);
			}
			catch (Exception)
			{
				return this.GeneralError();
			}
		}

		private async Task<string> UploadImage(IFormFile image, string entityType)
		{
			try
			{
				var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
				var imagePath = Path.Combine(this.hostingEnvironment.WebRootPath, $"images/{entityType}", fileName);

				await using (var fileStream = new FileStream(imagePath, FileMode.Create))
				{
					await image.CopyToAsync(fileStream);
				}

				return $"/images/{entityType}/" + fileName; // Return the relative path to the image
			}
			catch (Exception)
			{
				throw new FileNotFoundException("Error during image uploading to the file store.");
			}
		}

		public async Task<IActionResult> MakeDefaultById( int entityId, string entityType, int id)
		{
			var imageExists = await this.imageService.ImageExistByIdAsync(id);

			if (!imageExists)
			{
				this.TempData[ErrorMessage] = "Снимка с този идентификатор не съществува!";

				return this.RedirectToAction("Edit", entityType, new {id=entityId});
			}

			await this.imageService.SetDefaultImageAsync(entityId, entityType, id);

			this.TempData[SuccessMessage] = "Снимката за корица беше сменена успешно!";

			return this.RedirectToAction("Edit", entityType, new { id = entityId });

		}

		private IActionResult GeneralError()
		{
			this.TempData[ErrorMessage] =
				"Unexpected error occurred! Please try again later or contact administrator";

			return this.RedirectToAction("Index", "Home");
		}
	}
}
