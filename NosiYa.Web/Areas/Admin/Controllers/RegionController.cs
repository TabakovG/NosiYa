namespace NosiYa.Web.Areas.Admin.Controllers
{
	using Microsoft.AspNetCore.Mvc;

	using Common;
	using NosiYa.Services.Data.Interfaces;
	using NosiYa.Web.Controllers;
	using NosiYa.Web.ViewModels.Region;
	using static Common.NotificationMessagesConstants;

	public class RegionController : BaseAdminController
	{
		private readonly IRegionService regionService;
		private readonly IImageService imageService;
		private readonly IWebHostEnvironment webHostEnvironment;

		public RegionController(IRegionService regionService, IImageService imageService, IWebHostEnvironment webHostEnvironment)
		{
			this.regionService = regionService;
			this.imageService = imageService;
			this.webHostEnvironment = webHostEnvironment;
		}


		[HttpGet]
		public IActionResult Add()
		{
			try
			{
				var region = new RegionFormModel();
				return this.View(region);
			}
			catch (Exception)
			{
				return this.GeneralError();
			}
		}

		[HttpPost]
		public async Task<IActionResult> Add(RegionFormModel model, [FromForm] ICollection<IFormFile> elementImages)
		{

			if (!this.ModelState.IsValid)
			{
				return this.View(model);
			}

			try
			{
				int regionId =
					await this.regionService.CreateAndReturnIdAsync(model);

				//Add images to the event
				if (elementImages.Any())
				{
					// Call Add from ImageController without redirecting
					var imageController = new ImageController(imageService, webHostEnvironment);
					imageController.ControllerContext = ControllerContext;

					string entityType = EntityTypesConst.Region;

					// Invoke AddImagesOnEntityCreate Action 
					await imageController.AddImagesOnEntityCreateAsync(regionId, entityType, elementImages);

				}

				return this.RedirectToAction("Details", "Region", new { Id = regionId });
			}
			catch (Exception)
			{
				return this.GeneralError();

			}
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			try
			{
				var regionExists = await this.regionService.ExistsByIdAsync(id);

				if (!regionExists)
				{
					this.TempData[ErrorMessage] = "Регион с този идентификатор не съществува!";

					return this.RedirectToAction("All", "Region");
				}

				RegionFormModel formModel = await this.regionService
					.GetForEditByIdAsync(id);

				//Populate related Images
				formModel.Images = await this.imageService.GetRelatedImagesAsync(id, EntityTypesConst.Region);

				return View(formModel);
			}
			catch (Exception)
			{
				return this.GeneralError();
			}
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, RegionFormModel model, [FromForm] ICollection<IFormFile> elementImages)
		{
			if (!this.ModelState.IsValid)
			{
				return this.View(model);
			}

			try
			{
				var regionExists = await this.regionService.ExistsByIdAsync(id);

				if (!regionExists)
				{
					this.TempData[ErrorMessage] = "Регион с този идентификатор не съществува!";

					return this.RedirectToAction("All", "Region");
				}

				await this.regionService.EditByIdAsync(id, model);

				//Add images to the event
				if (elementImages.Count > 0)
				{
					// Call Add from ImageController without redirecting
					var imageController = new ImageController(imageService, webHostEnvironment);
					imageController.ControllerContext = ControllerContext;

					string entityType = EntityTypesConst.Region;

					// Invoke AddImagesOnEntityCreate Action 
					await imageController.UploadImagesAsync(id, entityType, elementImages);

				}

				this.TempData[SuccessMessage] = "Промените са запазени успешно!";
				return this.RedirectToAction("Details", "Region", new { Id = id });
			}
			catch (Exception)
			{
				this.ModelState.AddModelError(string.Empty,
					"Възникна грешка при обработване на заявката. Моля опитайте отново или се свържете с администратор!");

				return this.View(model);
			}
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				var regionExists = await this.regionService.ExistsByIdAsync(id);

				if (!regionExists)
				{
					this.TempData[ErrorMessage] = "Регион с този идентификатор не съществува!";

					return this.RedirectToAction("All", "Region");
				}

				RegionDetailsViewModel viewModel =
					await this.regionService.GetDetailsByIdAsync(id);

				return this.View(viewModel);
			}
			catch (Exception)
			{
				return this.GeneralError();
			}
		}

		[HttpPost]
		public async Task<IActionResult> Delete(RegionDetailsViewModel model)
		{
			try
			{
				var regionExists = await this.regionService.ExistsByIdAsync(model.Id);

				if (!regionExists)
				{
					this.TempData[ErrorMessage] = "Регион с този идентификатор не съществува!";

					return this.RedirectToAction("All", "Region");
				}

				await this.regionService.DeleteByIdAsync(model.Id);

				this.TempData[WarningMessage] = "Регионa беше изтрит успешно!";
				return this.RedirectToAction("All", "Region");
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
