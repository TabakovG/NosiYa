namespace NosiYa.Web.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Authorization;

	using NosiYa.Services.Data.Interfaces;
	using Infrastructure.Extensions;
	using ViewModels.Cart;
	using static Common.NotificationMessagesConstants;
	using static Common.SeedingConstants;


	[Authorize(Roles = $"{AdminRoleName}, {UserRoleName}")]
	public class CartController : BaseController
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

			ICollection<CartItemsViewModel> orderModel = await this.cartService.GetAllItemsFromUserCartAsync(this.User!.GetId()!);

			return View(orderModel);
		}

		[HttpGet]
		public async Task<IActionResult> Add(int id)
		{
			try
			{

				var rentModel = new CartPreOrderFormModel
				{
					OutfitModel = await this.outfitSetService.GetForRentByIdAsync(id),
					CardItemFormModel = new CartItemFormModel
					{
						OutfitSetId = id,
						FromDate = DateTime.UtcNow,
						ToDate = DateTime.UtcNow
						
					}
				};

				return this.View(rentModel);
			}
			catch (Exception)
			{
				return this.GeneralError();
			}
		}

		[HttpPost]
		public async Task<IActionResult> Add(CartItemFormModel model)
		{
			try
			{
				var isAuthenticated = this.User?.Identity?.IsAuthenticated ?? false;

				if (this.ModelState.IsValid && isAuthenticated)
				{

					model.CartId = await this.cartService.GetCartIdByUserIdAsync(this.User!.GetId()!);

					await this.cartService.CreateCartItemAsync(model);

					return this.RedirectToAction("All", "OutfitSet");

				}

				var rentModel = new CartPreOrderFormModel
				{
					OutfitModel = await this.outfitSetService.GetForRentByIdAsync(model.OutfitSetId),
					CardItemFormModel = model
				};

				return this.View(rentModel);
			}
			catch (Exception)
			{
				return this.GeneralError();

			}
		}



		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var itemExistsById = await this.cartService
				.CartItemExistsById(id);

			if (!itemExistsById)
			{
				this.TempData[ErrorMessage] = "Продукт с този идентификатор не съществува!";

				return this.RedirectToAction("Items", "Cart");
			}

			try
			{
				var formModel = new CartPreOrderFormModel
				{
					CardItemFormModel = await this.cartService
						.GetForEditByIdAsync(id)
				};
				formModel.OutfitModel = await this.outfitSetService
					.GetForRentByIdAsync(formModel.CardItemFormModel.OutfitSetId);


				return View(formModel);
			}
			catch (Exception)
			{
				return this.GeneralError();
			}
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, CartItemFormModel model)
		{
			try
			{
				if (!this.ModelState.IsValid)
				{
					var formModel = new CartPreOrderFormModel
					{
						CardItemFormModel = model,
						OutfitModel = await this.outfitSetService
							.GetForRentByIdAsync(model.OutfitSetId)
					};
					return this.View(formModel);
				}
				var isAuthenticated = this.User?.Identity?.IsAuthenticated ?? false;

				if (!isAuthenticated || !this.ModelState.IsValid)
				{
					var formModel = new CartPreOrderFormModel
					{
						CardItemFormModel = model,
						OutfitModel = await this.outfitSetService
							.GetForRentByIdAsync(model.OutfitSetId)
					};
					return this.View(formModel);
				}

				var itemExistsById = await this.cartService
					.CartItemExistsById(id);

				if (!itemExistsById)
				{
					this.TempData[ErrorMessage] = "Продукт с този идентификатор не съществува!";

					return this.RedirectToAction("Items", "Cart");
				}

				await this.cartService.EditByIdAsync(id, model);

				this.TempData[SuccessMessage] = "Промените са запазени успешно!";

				return this.RedirectToAction("Items", "Cart");
			}
			catch (Exception)
			{
				this.ModelState.AddModelError(string.Empty,
					"Възникна грешка при обработване на заявката. Моля опитайте отново или се свържете с администратор!");

				var formModel = new CartPreOrderFormModel
				{
					CardItemFormModel = model,
					OutfitModel = await this.outfitSetService
						.GetForRentByIdAsync(model.OutfitSetId)
				};
				return this.View(formModel);
			}
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			var isAuthenticated = this.User?.Identity?.IsAuthenticated ?? false;

			if (!isAuthenticated)
			{
				return RedirectToAction("All", "OutfitSet");
			}

			var cartItem = await this.cartService
				.CartItemExistsById(id);

			if (!cartItem)
			{
				this.TempData[ErrorMessage] = "Продукт с този идентификатор не съществува!";

				return this.RedirectToAction("Items", "Cart");
			}


			try
			{
				await this.cartService.DeleteItemFromUserCartAsync(id);

				this.TempData[WarningMessage] = "Продукта беше изтрит успешно!";

				return this.RedirectToAction("Items", "Cart");
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
