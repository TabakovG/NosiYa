
using NosiYa.Web.ViewModels.Region;

namespace NosiYa.Web.Controllers
{
	using Microsoft.AspNetCore.Mvc.Infrastructure;
	using Microsoft.AspNetCore.Mvc;

	using NosiYa.Services.Data.Interfaces;
	using ViewModels.OutfitSet;

	public class RegionController : Controller
	{
		private readonly IRegionService regionService;

		public RegionController(IRegionService regionService)
		{
			this.regionService = regionService;
		}

		public async Task<IActionResult> All([FromQuery] AllRegionsPaginatedModel model)
		{
			var query = await this.regionService.AllAvailableRegionsAsync(model);

			model.Regions = query ?? new HashSet<RegionAllViewModel>();
			model.RegionsCount = model.Regions.Count();

			return View(model);

		}
	}
}
