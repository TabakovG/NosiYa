using Newtonsoft.Json;
using NosiYa.Services.Data.Interfaces;
using NosiYa.Web.ViewModels.Image;
using NuGet.Protocol;

namespace NosiYa.Web.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using Infrastructure.Extensions;

	public class ImageController : Controller
	{
		private readonly IImageService imageService;
		private readonly IWebHostEnvironment hostingEnvironment;

		public ImageController(IImageService imageService, IWebHostEnvironment hostingEnvironment)
		{
			this.imageService = imageService;
			this.hostingEnvironment = hostingEnvironment;
		}

		[HttpPost]
		public async Task<List<string>> AddImagesOnEntityCreate(int entityId, string entityType, List<IFormFile> elementImages)
		{
			var result = new List<string>();
			try
			{
				foreach (var imageStr in elementImages)
				{

					var imagePath = SaveImageToWwwRoot(imageStr, entityType);
					result.Add(imagePath);

					var imageModel = new ImageFormModel
					{
						Url = imagePath,
					};

					switch (entityType)
					{
						case "set":
							imageModel.OutfitSetId = entityId;
							break;
						case "part":
							imageModel.OutfitPartId = entityId;
							break;
						case "event":
							imageModel.EventId = entityId;
							break;
						case "region":
							imageModel.RegionId = entityId;
							break;
						default:
							continue;
					}

					var userId = Guid.Parse(this.User!.GetId()!);

					await this.imageService.AddImageAsync(imageModel, userId);
				}

				await this.imageService.SetDefaultImageAsync(entityId, entityType, null);
			}
			catch (Exception)
			{
				this.TempData["ErrorMessage"] =
					"Unexpected error occurred during the images processing! Please try again later or contact administrator";
			}

			return result;
		}

		[HttpPost]
		public async Task<IActionResult> Upload(ImageFormModel model, string returnUrl)
		{
			try
			{
				//TODO To check if the user is admin

				if (!this.ModelState.IsValid)
				{
					return Redirect(returnUrl);

				}

				var isAuthenticated = this.User?.Identity?.IsAuthenticated ?? false;

				if (!isAuthenticated)
				{
					return Redirect(returnUrl);
				}

				try
				{
					var userId = Guid.Parse(this.User!.GetId()!);
					await this.imageService.AddImageAsync(model, userId);

					return Redirect(returnUrl);
				}
				catch (Exception)
				{
					return Redirect(returnUrl);
				}
			}
			catch (Exception)
			{
				return this.GeneralError();

			}
		}

		private string SaveImageToWwwRoot(IFormFile image, string entityType)
		{
			var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
			var imagePath = Path.Combine(this.hostingEnvironment.WebRootPath, $"images/{entityType}", fileName);

			using (var fileStream = new FileStream(imagePath, FileMode.Create))
			{
				image.CopyTo(fileStream);
			}

			return $"/images/{entityType}/" + fileName; // Return the relative path to the image
		}

		private IActionResult GeneralError()
		{
			this.TempData["ErrorMessage"] =
				"Unexpected error occurred! Please try again later or contact administrator";

			return this.RedirectToAction("Index", "Home");
		}
	}
}
