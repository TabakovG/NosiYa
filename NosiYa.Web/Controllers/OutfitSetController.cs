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

		public async Task<IActionResult> All([FromQuery] AllOutfitsQueryModel queryModel)
		{
			var queryAndSorting = await this.outfitService.AllAvailableOutfitSetsAsync(queryModel);

			queryModel.Outfits = queryAndSorting.OutfitSets;
			queryModel.OutfitsCount = queryAndSorting.TotalOutfits;
			queryModel.Regions = await this.regionService.GetAllRegionsNamesAsync();

			return View(queryModel);
		}
	}
}
