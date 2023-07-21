namespace NosiYa.Web.Controllers
{
	using Microsoft.AspNetCore.Mvc;

	using NosiYa.Services.Data.Interfaces;
	using ViewModels.Region;

	public class RegionController : Controller
	{
		private readonly IRegionService regionService;

		public RegionController(IRegionService regionService)
		{
			this.regionService = regionService;
		}

		public async Task<IActionResult> All([FromQuery] AllRegionsPaginatedModel model)
		{
			var serviceModel = await this.regionService.AllAvailableRegionsAsync(model);

			model.Regions = serviceModel.Regions;
			model.RegionsCount = serviceModel.RegionsCount;

			return View(model);

		}

		[HttpGet]
		public async Task<IActionResult> Add()
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
		public async Task<IActionResult> Add(RegionFormModel model)
		{
			try
			{
				//TODO To check if the user is admin

				if (!this.ModelState.IsValid)
				{
					return this.View(model);
				}

				var isAuthenticated = this.User?.Identity?.IsAuthenticated ?? false;

				if (!isAuthenticated)
				{
					return this.View(model);
				}

				try
				{
					int regionId =
						await this.regionService.CreateAndReturnIdAsync(model);

					return this.RedirectToAction("Details", "Region", new { Id = regionId });
				}
				catch (Exception)
				{
					return this.View(model);
				}
			}
			catch (Exception)
			{
				return this.GeneralError();

			}
		}

		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			var regionExists = await this.regionService.ExistsByIdAsync(id);

			if (!regionExists)
			{
				this.TempData["ErrorMessage"] = "Регионът не съществува!";

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


		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var regionExists = await this.regionService.ExistsByIdAsync(id);

			if (!regionExists)
			{
				this.TempData["ErrorMessage"] = "Регион с този идентификатор не съществува!";

				return this.RedirectToAction("All", "Region");
			}

			try
			{
				RegionFormModel formModel = await this.regionService
					.GetForEditByIdAsync(id);
				return View(formModel);
			}
			catch (Exception)
			{
				return this.GeneralError();
			}
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, RegionFormModel model)
		{
			if (!this.ModelState.IsValid)
			{
				return this.View(model);
			}

			var regionExists = await this.regionService.ExistsByIdAsync(id);

			if (!regionExists)
			{
				this.TempData["ErrorMessage"] = "Регион с този идентификатор не съществува!";

				return this.RedirectToAction("All", "Region");
			}

			var isAuthenticated = this.User?.Identity?.IsAuthenticated ?? false;

			if (!isAuthenticated)
			{
				return this.View(model);
			}

			try
			{
				await this.regionService.EditByIdAsync(id, model);
				this.TempData["SuccessMessage"] = "Промените са запазени успешно!";
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

			var regionExists = await this.regionService.ExistsByIdAsync(id);

			if (!regionExists)
			{
				this.TempData["ErrorMessage"] = "Регион с този идентификатор не съществува!";

				return this.RedirectToAction("All", "Region");
			}

			try
			{
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
		public async Task<IActionResult> Delete(int id, RegionDetailsViewModel model)
		{
			var regionExists = await this.regionService.ExistsByIdAsync(id);

			if (!regionExists)
			{
				this.TempData["ErrorMessage"] = "Регион с този идентификатор не съществува!";

				return this.RedirectToAction("All", "Region");
			}

			try
			{
				await this.regionService.DeleteByIdAsync(id);

				this.TempData["WarningMessage"] = "Регионa беше изтрит успешно!";
				return this.RedirectToAction("All", "Region");
			}
			catch (Exception)
			{
				return this.GeneralError();
			}
		}

		private IActionResult GeneralError()
		{
			this.TempData["ErrorMessage"] =
				"Unexpected error occurred! Please try again later or contact administrator";

			return this.RedirectToAction("Index", "Home");
		}
	}
}
