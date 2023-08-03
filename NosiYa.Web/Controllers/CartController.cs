﻿namespace NosiYa.Web.Controllers
{
	using Microsoft.AspNetCore.Mvc;

	using NosiYa.Services.Data.Interfaces;
	using Infrastructure.Extensions;
	using ViewModels.Cart;
	using static Common.NotificationMessagesConstants;
    using Microsoft.AspNetCore.Authorization;
	using static Common.SeedingConstants;
    using Microsoft.AspNetCore.Hosting;
    using NosiYa.Common;
    using NosiYa.Services.Data;
    using NosiYa.Web.ViewModels.Region;

    [Authorize(Roles = $"{AdminRoleName}, {UserRoleName}")]
    public class CartController : BaseController
    {
		private readonly ICartService cartService;
		private readonly IOutfitSetService outfitSetService;
		private readonly ICalendarService calendarService;

		public CartController(ICartService cartService, IOutfitSetService outfitSetService, ICalendarService calendarService)
		{
			this.outfitSetService = outfitSetService;
			this.cartService = cartService;
			this.calendarService = calendarService;
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
        public async Task<IActionResult> Mine()
		{
			var isAuthenticated = this.User?.Identity?.IsAuthenticated ?? false;

			if (!isAuthenticated)
			{
				return this.RedirectToAction("Index", "Home"); //TODO to login page
			}

			ICollection<ReservedItemsViewModel> orderModel = await this.cartService.GetReservedItemsByUserIdAsync(this.User!.GetId()!);

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

			var orderExists = await this.cartService.CartItemExistsById(id);
			if (!orderExists)
			{
				this.TempData[ErrorMessage] = "Поръчка с посочения идентификатор не съществува!";
				return this.RedirectToAction("Index", "Home");
			}

			//TODO validate dates
			
			try
			{
				var stillFree = await this.calendarService.ValidateDatesAsync(model.FromDate, model.ToDate, model.OutfitId);

				if (stillFree)
				{
					await this.cartService.CartOrderCompleteAsync(model, this.User!.GetId()!);
					await this.cartService.DeleteItemFromUserCartAsync(id);
					return this.RedirectToAction("Items", "Cart");
				}
				this.TempData[ErrorMessage] = "Носията вече е заета за посочените дати!";

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
                CartPreOrderFormModel formModel = await this.cartService
                    .GetForEditByIdAsync(id);

                return View(formModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CartPreOrderFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var itemExistsById = await this.cartService
                .CartItemExistsById(id);

            if (!itemExistsById)
            {
                this.TempData[ErrorMessage] = "Продукт с този идентификатор не съществува!";

                return this.RedirectToAction("Items", "Cart");
            }

            var isAuthenticated = this.User?.Identity?.IsAuthenticated ?? false;

            if (!isAuthenticated)
            {
                return this.View(model);
            }

            try
            {
                await this.cartService.EditByIdAsync(id, model);

                this.TempData[SuccessMessage] = "Промените са запазени успешно!";

                return this.RedirectToAction("Items", "Cart");
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty,
                    "Възникна грешка при обработване на заявката. Моля опитайте отново или се свържете с администратор!");

                return this.View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
		{
			var cartItem = await this.cartService
				.CartItemExistsById(id);

			if (!cartItem)
			{
				this.TempData[ErrorMessage] = "Продукт с този идентификатор не съществува!";

				return this.RedirectToAction("Items", "Cart");
			}

			var isAuthenticated = this.User?.Identity?.IsAuthenticated ?? false;

			if (!isAuthenticated)
			{
				return RedirectToAction("All", "OutfitSet"); //TODO better to login page
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
