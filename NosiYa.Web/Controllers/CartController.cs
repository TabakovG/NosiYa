using Newtonsoft.Json;
using NosiYa.Services.Data.Interfaces;
using NosiYa.Web.Infrastructure.Extensions;
using NosiYa.Web.ViewModels.Cart;

namespace NosiYa.Web.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using NosiYa.Web.ViewModels.Region;
	using System.Globalization;

	public class CartController : Controller
	{
		private readonly ICartService cartService;
		private readonly IOutfitSetService outfitSetService;

		public CartController(ICartService cartService, IOutfitSetService outfitSetService)
		{
			this.outfitSetService = outfitSetService;
			this.cartService = cartService;
		}

		[HttpGet]
		public async Task<IActionResult> Items()
		{
			var isAuthenticated = this.User?.Identity?.IsAuthenticated ?? false;

			if (!isAuthenticated)
			{
				return this.RedirectToAction("Index", "Home"); //TODO to login page
			}

			ICollection<CartPreOrderViewModel> orderModel = await this.cartService.GetAllItemsFromUserCartAsync(this.User!.GetId()!);

			return View(orderModel);
		}

		[HttpPost]

		public async Task<IActionResult> Order(int id,[FromForm] CartCompleteOrderFormModel model)
		{
			if (!this.ModelState.IsValid)
			{
				return this.RedirectToAction("Items", "Cart");
			}
			var isAuthenticated = this.User?.Identity?.IsAuthenticated ?? false;

			if (!isAuthenticated)
			{
				return this.RedirectToAction("Index", "Home");
			}

			var orderExists = await this.cartService.OrderExistsById(id);
			if (!orderExists)
			{
				this.TempData["ErrorMessage"] = "Поръчка с посочения идентификатор не съществува!";
				return this.RedirectToAction("Index", "Home");
			}

			//TODO validate dates
			
			try
			{
				await this.cartService.CartOrderCompleteAsync(model, this.User!.GetId()!);
				await this.cartService.DeleteItemFromUserCartAsync(id);
				return this.RedirectToAction("Items", "Cart");

			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}

		}

		[HttpGet]
		public async Task<IActionResult> Add(int id)
		{
			try
			{
				var outfit = await this.outfitSetService.GetForRentByIdAsync(id);

				var rentModel = new CartPreOrderFormModel
				{
					OutfitModel = outfit
				};


				return this.View(rentModel);
			}
			catch (Exception)
			{
				return this.GeneralError();
			}
		}

		[HttpPost]
		public async Task<IActionResult> Add(CartPreOrderFormModel model)
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

				model.CartId = await this.cartService.GetCartIdByUserIdAsync(this.User!.GetId()!);

				try
				{
					await this.cartService.CreateCartItemAsync(model);

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

		[HttpGet]
		public async Task<string> PopulateCalendar(string start, string end)
		{
			try
			{
				DateTime startDate;
				DateTime endDate;

				if (!DateTime.TryParseExact(start, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate))
				{
					this.TempData["ErrorMessage"] =
						"Unexpected error occurred during calendar population request! ";
					return "";
				}
				if (!DateTime.TryParseExact(end, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate))
				{
					this.TempData["ErrorMessage"] =
						"Unexpected error occurred during calendar population request! ";
					return "";
				}

				var reservedDates = await this.cartService.GetReservedDates(startDate, endDate);

				return JsonConvert.SerializeObject(reservedDates);
			}
			catch (Exception)
			{
				this.TempData["ErrorMessage"] =
					"Unexpected error occurred during calendar population! ";
				return "";
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
