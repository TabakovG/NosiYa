namespace NosiYa.Web.Controllers
{
	using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

	using NosiYa.Services.Data.Interfaces;
	using ViewModels.Region;
	using static Common.NotificationMessagesConstants;
	using static Common.SeedingConstants;

    [Authorize(Roles = AdminRoleName)]
    public class RegionController : BaseController
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

        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery] AllRegionsPaginatedModel model)
		{
			var serviceModel = await this.regionService.AllAvailableRegionsAsync(model);

			model.Regions = serviceModel.Regions;
			model.RegionsCount = serviceModel.RegionsCount;

			return View(model);

		}

		[HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
		{
			var regionExists = await this.regionService.ExistsByIdAsync(id);

			if (!regionExists)
			{
				this.TempData[ErrorMessage] = "Регионът не съществува!";

				return this.RedirectToAction("All", "Region");
			}

			try
			{
				RegionDetailsViewModel viewModel = await this.regionService
					.GetDetailsByIdAsync(id);


				return View(viewModel);
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
