using NosiYa.Services.Data.Interfaces;
using NosiYa.Web.ViewModels.OutfitPart;
using NuGet.Protocol;

namespace NosiYa.Web.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using NosiYa.Services.Data.Model;
	using NosiYa.Web.ViewModels.OutfitSet;

	public class OutfitPartController : Controller
	{
		private readonly IOutfitPartService outfitPartService;

		public OutfitPartController(IOutfitPartService outfitPartService)
		{
			this.outfitPartService = outfitPartService;
		}

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

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			bool outfitPartExists = await this.outfitPartService
				.ExistByIdAsync(id);
			if (!outfitPartExists)
			{
				this.TempData["ErrorMessage"] = "Елемент на носия с този идентификатор не съществува!";

				return this.RedirectToAction("All", "OutfitSet");
			}

			try
			{
				OutfitPartForDelete viewModel =
					await this.outfitPartService.GetOutfitPartForDeleteAsync(id);

				return this.View(viewModel);
			}
			catch (Exception)
			{
				return this.GeneralError();
			}
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id, OutfitSetForDelete model)
		{
			bool outfitPartExists = await this.outfitPartService
				.ExistByIdAsync(id);
			if (!outfitPartExists)
			{
				this.TempData["ErrorMessage"] = "Елемент на носия с този идентификатор не съществува!";

				return this.RedirectToAction("All", "OutfitSet");
			}

			try
			{
				var outfitSetId = await this.outfitPartService.DeleteByIdAsyncAndReturnParentId(id);

				this.TempData["WarningMessage"] = "Елементът беше изтрит успешно!";
				return this.RedirectToAction("Details", "OutfitSet", new {Id = outfitSetId});
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
