using NosiYa.Services.Data.Interfaces;
using NosiYa.Web.Infrastructure.Extensions;

namespace NosiYa.Web.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using ViewModels.Rent;

	public class RentController : Controller
	{
		private readonly IRenterService renterService;
		private readonly IOutfitSetService outfitSetService;

		public RentController(IRenterService renterService, IOutfitSetService outfitSetService )
		{
			this.outfitSetService = outfitSetService;
			this.renterService = renterService;
		}

		[HttpGet]
		public async Task<IActionResult> Add(int id)
		{
			try
			{
				var outfit = await this.outfitSetService.GetForRentByIdAsync(id);
				var reservedDates = await this.renterService.GetReservedDates();

				var rentModel = new CartFormModel
				{
					OutfitModel = outfit,
					ReservedDates = reservedDates,
				};

				

				return this.View(rentModel);
			}
			catch (Exception)
			{
				return this.GeneralError();
			}
		}

		[HttpPost]
		public async Task<IActionResult> Add(CartFormModel model)
		{
			try
			{

				if (!this.ModelState.IsValid)
				{
					return this.View(model);
				}

				var isAuthenticated = this.User?.Identity?.IsAuthenticated ?? false;

				if (!isAuthenticated)
				{
					return this.View(model);
				}
				
				model.CartId = await this.renterService.GetCartIdByUserIdAsync(this.User!.GetId()!);

				try
				{
					await this.renterService.CreateRentRequestAsync(model);

					return this.RedirectToAction("All", "OutfitSet");
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

		private IActionResult GeneralError()
		{
			this.TempData["ErrorMessage"] =
				"Unexpected error occurred! Please try again later or contact administrator";

			return this.RedirectToAction("Index", "Home");
		}
	}
}
