namespace NosiYa.Web.Controllers
{
	using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

	using NosiYa.Services.Data.Interfaces;

	using ViewModels.OutfitPart;
	using static Common.NotificationMessagesConstants;

    public class OutfitPartController : BaseController
    {
		private readonly IOutfitPartService outfitPartService;


		public OutfitPartController(IOutfitPartService outfitPartService)
		{
			this.outfitPartService = outfitPartService;
		}

		[HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
		{
			var outfitPartExists = await this.outfitPartService.ExistByIdAsync(id);

			if (!outfitPartExists)
			{
				this.TempData[ErrorMessage] = "Елемент на носия с този идентификатор не съществува!";

				return this.RedirectToAction("All", "OutfitSet");
			}

			try
			{
				OutfitPartDetailsViewModel viewModel = await this.outfitPartService
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
