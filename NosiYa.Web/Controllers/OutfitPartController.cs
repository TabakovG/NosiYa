using NosiYa.Web.ViewModels.OutfitPart;

namespace NosiYa.Web.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using NosiYa.Services.Data.Model;
	using NosiYa.Web.ViewModels.OutfitSet;

	public class OutfitPartController : Controller
	{
		[HttpGet]
		public async Task<IActionResult> Add(CreateSetToAddPartServiceModel serviceModel)
		{
			try
			{
				var outfitPart = new OutfitPartFormModel();
				ViewData["SetId"] = serviceModel.OutfitSetId;
				ViewData["NumberOfParts"] = serviceModel.NumberOfParts;
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
