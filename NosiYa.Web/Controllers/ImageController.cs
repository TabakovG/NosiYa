namespace NosiYa.Web.Controllers
{
	using System.Net;
	using System.Collections.Generic;
	using Microsoft.AspNetCore.Mvc;

	using Infrastructure.Extensions;

	using NosiYa.Services.Data.Interfaces;
	using ViewModels.Image;
	using static Common.EntityTypesConst;
	using static Common.EntityValidationConstants.Image;
	using static Common.NotificationMessagesConstants;

	using SixLabors.ImageSharp.Formats.Jpeg;
	using SixLabors.ImageSharp.Processing;


	public class ImageController : BaseController
	{
		private readonly IImageService imageService;
		private readonly IWebHostEnvironment hostingEnvironment;

		public ImageController(IImageService imageService, IWebHostEnvironment hostingEnvironment)
		{
			this.imageService = imageService;
			this.hostingEnvironment = hostingEnvironment;
		}

		[HttpPost]
		public async Task AddImagesOnEntityCreateAsync(int entityId, string entityType, ICollection<IFormFile> elementImages)
		{
			try
			{
				await this.UploadImagesAsync(entityId, entityType, elementImages);
				await this.imageService.SetDefaultImageAsync(entityId, entityType);
			}
			catch (Exception)
			{
				this.TempData[ErrorMessage] =
					"Unexpected error occurred during the images processing! Please try again later or contact administrator";
			}
		}

		[HttpGet]
		public async Task<bool> FormatIsValid(IFormFile file)
		{
			await using var fileStream = file.OpenReadStream();
			var fileBytes = new byte[2];

			await fileStream.ReadAsync(fileBytes, 0, 2);

			if (fileBytes.Length < 2)
			{
				return false;
			}

			// Check the start of the file (first two bytes) for "FF D8"
			if (fileBytes[0] == 0xFF && fileBytes[1] == 0xD8)
			{
				// Move the file stream position to the last two bytes
				fileStream.Seek(-2, SeekOrigin.End);

				// Read the last two bytes
				await fileStream.ReadAsync(fileBytes, 0, 2);

				// Check the end of the file (last two bytes) for "FF D9"
				if (fileBytes[0] == 0xFF && fileBytes[1] == 0xD9)
				{
					return true;
				}
			}

			return false;
		}

		[HttpPost]
		public async Task UploadImagesAsync(int entityId, string entityType, ICollection<IFormFile> elementImages)
		{
			try
			{
				foreach (var imageFormFile in elementImages)
				{
					var formatIsValid = await this.FormatIsValid(imageFormFile);
					if (!formatIsValid)
					{
						this.TempData[WarningMessage] = "В момента можете да качите само снимки във формат 'JPEG'";
						continue;
					}

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
			try
			{
				var imageExists = await this.imageService.ImageExistByIdAsync(id);

				if (!imageExists)
				{
					this.TempData[ErrorMessage] = "Снимка с този идентификатор не съществува!";

					return this.Redirect(returnUrl);
				}

				await this.imageService.DeleteImageByIdAsync(id, this.hostingEnvironment.WebRootPath);

				this.TempData[WarningMessage] = "Снимката беше изтрита успешно!";
				return this.Redirect(returnUrl);
			}
			catch (Exception)
			{
				return this.GeneralError();
			}
		}

		[HttpPost]
		private async Task<string> UploadImage(IFormFile file, string entityType)
		{
			try
			{
				if (file.Length == 0)
				{
					throw new FileNotFoundException("File with 0 length can't be processed!");
				}

				var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
				var imagePath = Path.Combine(this.hostingEnvironment.WebRootPath, $"images/{entityType}", fileName);

				using (var image = await SixLabors.ImageSharp.Image.LoadAsync(file.OpenReadStream()))
				{
					// Resize the image to your desired dimensions.
					int targetWidth = ImageResizeMaxWidth;
					int targetHeight = ImageResizeMaxHeight;


					// Calculate new dimensions while maintaining the aspect ratio.
					if (image.Width > image.Height)
					{
						targetHeight = (int)((float)image.Height / image.Width * targetWidth);
					}
					else
					{
						targetWidth = (int)((float)image.Width / image.Height * targetHeight);
					}

					image.Mutate(x => x.Resize(targetWidth, targetHeight));

					// Save the resized image to the server.
					await using (var fileStream = new FileStream(imagePath, FileMode.Create))
					{
						await image.SaveAsync(fileStream, new JpegEncoder());
					}
				}

				return $"/images/{entityType}/" + fileName; // Return the relative path to the image

			}
			catch (Exception)
			{
				throw new FileNotFoundException("Error during image uploading to the file store.");
			}
		}

		[HttpPost]
		public async Task<IActionResult> MakeDefaultById(int entityId, string entityType, int id, string area)
		{
			try
			{
				var imageExists = await this.imageService.ImageExistByIdAsync(id);

				if (!imageExists)
				{
					this.TempData[ErrorMessage] = "Снимка с този идентификатор не съществува!";
					return this.RedirectToAction("Edit", entityType, new { id = entityId, Area = area });
				}

				entityType = WebUtility.HtmlEncode(entityType).ToLower();
				var options = new HashSet<string>()
				{
					"outfitset", "outfitpart", "event", "region"
				};
				if (!options.Contains(entityType))
				{
					return this.GeneralError();
				}

				await this.imageService.SetDefaultImageAsync(entityId, entityType, id);
				this.TempData[SuccessMessage] = "Снимката за корица беше сменена успешно!";
				return this.RedirectToAction("Edit", entityType, new { id = entityId, Area = area });
			}
			catch (Exception)
			{
				return this.GeneralError();
			}
		}

		private IActionResult GeneralError()
		{
			this.TempData[ErrorMessage] =
				"Unexpected error occurred! Please try again later or contact administrator";

			return this.RedirectToAction("Index", "Home");
		}
	}
}
