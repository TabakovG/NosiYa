using NosiYa.Web.ViewModels.OutfitPart;
using NuGet.Protocol;

namespace NosiYa.Web.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using NosiYa.Services.Data.Model;
	using NosiYa.Web.ViewModels.OutfitSet;

	public class OutfitPartController : Controller
	{
		[HttpGet]
		public async Task<IActionResult> Add(int setId)
		{
			try
			{
				var outfitPart = new OutfitPartFormModel();
				outfitPart.OutfitSetId = setId;
					return this.View(outfitPart);
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
