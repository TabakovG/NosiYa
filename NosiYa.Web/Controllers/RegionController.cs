namespace NosiYa.Web.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Authorization;

	using NosiYa.Services.Data.Interfaces;
	using ViewModels.Region;
	using static Common.NotificationMessagesConstants;

	public class RegionController : BaseController
	{
		private readonly IRegionService regionService;

		public RegionController(IRegionService regionService)
		{
			this.regionService = regionService;

		}

		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> All([FromQuery] AllRegionsPaginatedModel model)
		{
			try
			{
				var serviceModel = await this.regionService.AllAvailableRegionsAsync(model);

				model.Regions = serviceModel.Regions;
				model.RegionsCount = serviceModel.RegionsCount;

				return View(model);
			}
			catch (Exception)
			{
				return this.GeneralError();
			}
		}

		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> Details(int id)
		{
			try
			{
				var regionExists = await this.regionService.ExistsByIdAsync(id);

				if (!regionExists)
				{
					this.TempData[ErrorMessage] = "Регионът не съществува!";

					return this.RedirectToAction("All", "Region");
				}

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
