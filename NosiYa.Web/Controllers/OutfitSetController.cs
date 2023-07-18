using NosiYa.Data.Models.Enums;

namespace NosiYa.Web.Controllers
{
	using NosiYa.Services.Data.Interfaces;
	using ViewModels.OutfitSet;
	using Microsoft.AspNetCore.Mvc;

	public class OutfitSetController : Controller
	{
		private readonly IOutfitSetService outfitService;
		private readonly IRegionService regionService;

		public OutfitSetController(IOutfitSetService outfitService, IRegionService regionService)
		{
			this.outfitService = outfitService;
			this.regionService = regionService;
		}
		[HttpGet]
		public async Task<IActionResult> All([FromQuery] AllOutfitsQueryModel queryModel)
		{
			var queryAndSorting = await this.outfitService.AllAvailableOutfitSetsAsync(queryModel);

			queryModel.Outfits = queryAndSorting.OutfitSets;
			queryModel.OutfitsCount = queryAndSorting.TotalOutfits;
			queryModel.Regions = await this.regionService.GetAllRegionsNamesAsync();

			return View(queryModel);
		}

		[HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                var outfitSet = new OutfitSetFormModel
                {
                    Regions = await this.regionService.GetAllRegionsAsync()
                };

                return this.View(outfitSet);
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
