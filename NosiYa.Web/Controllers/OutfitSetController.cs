namespace NosiYa.Web.Controllers
{
	using Microsoft.AspNetCore.Authorization;

	using NosiYa.Services.Data.Interfaces;
	using ViewModels.OutfitSet;
	using Microsoft.AspNetCore.Mvc;
	using NosiYa.Services.Data.Models;
	using static Common.NotificationMessagesConstants;
	using static Common.SeedingConstants;

	public class OutfitSetController : BaseController
	{
		private readonly IOutfitSetService outfitService;
		private readonly IRegionService regionService;
		private readonly IOutfitPartService partService;


		public OutfitSetController(IOutfitSetService outfitService, IRegionService regionService, IOutfitPartService partService)
		{
			this.outfitService = outfitService;
			this.regionService = regionService;
			this.partService = partService;

		}

		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> All([FromQuery] AllOutfitsQueryModel queryModel)
		{
			try
			{
				AllOutfitsFilteredAndPagedServiceModel queryAndSorting = await this.outfitService.AllFilteredOutfitSetsByAvailabilityAsync(queryModel, true);

				queryModel.Outfits = queryAndSorting.OutfitSets;
				queryModel.OutfitsCount = queryAndSorting.TotalOutfits;
				queryModel.Regions = await this.regionService.GetAllRegionsNamesAsync();

				return View(queryModel);
			}
			catch (Exception)
			{
				return this.GeneralError();
			}
		}

		[HttpGet]
		[Authorize(Roles = AdminRoleName)]
		public async Task<IActionResult> AllUnavailable([FromQuery] AllOutfitsQueryModel queryModel)
		{
			try
			{
				AllOutfitsFilteredAndPagedServiceModel queryAndSorting = await this.outfitService.AllFilteredOutfitSetsByAvailabilityAsync(queryModel, false);

				queryModel.Outfits = queryAndSorting.OutfitSets;
				queryModel.OutfitsCount = queryAndSorting.TotalOutfits;
				queryModel.Regions = await this.regionService.GetAllRegionsNamesAsync();

				return View("All", queryModel);
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
				var outfitSetExists = await this.outfitService.ExistByIdAsync(id);

				if (!outfitSetExists)
				{
					this.TempData[ErrorMessage] = "Носия с този идентификатор не съществува!";

					return this.RedirectToAction("All", "OutfitSet");
				}

				OutfitSetDetailsViewModel viewModel = await this.outfitService
					.GetDetailsByIdAsync(id);
				viewModel.OutfitParts = await this.partService.GetAllPartsBySetIdAsync(id);
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
