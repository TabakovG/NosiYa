namespace NosiYa.Web.Controllers
{
	using Microsoft.AspNetCore.Mvc;

	using NosiYa.Services.Data.Interfaces;
	using Infrastructure.Extensions;
	using ViewModels.Cart;
	using static Common.NotificationMessagesConstants;
	using static Common.ApplicationConstants;
	using static Common.SeedingConstants;
	using ViewModels.Order;

	public class OrderController : BaseController
	{
		private readonly ICartService cartService;
		private readonly IOrderService orderService;
		private readonly ICalendarService calendarService;

		public OrderController(ICartService cartService, IOrderService orderService, ICalendarService calendarService)
		{
			this.cartService = cartService;
			this.calendarService = calendarService;
			this.orderService = orderService;
		}

		[HttpGet]
		public async Task<IActionResult> Mine()
		{
			var isAuthenticated = this.User?.Identity?.IsAuthenticated ?? false;

			if (!isAuthenticated)
			{
				return this.RedirectToAction("Index", "Home"); //TODO to login page
			}

			try
			{
				ICollection<OrderViewModel> orderModel = await this.orderService.GetOrdersByUserIdAsync(this.User!.GetId()!);

				return View(orderModel);
			}
			catch (Exception)
			{
				return this.GeneralError();
			}

		}

		[HttpPost]
		public async Task<IActionResult> Add(int id, [FromForm] CartCompleteOrderFormModel model)
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
			try
			{
				var orderExists = await this.cartService.CartItemExistsById(id);
				if (!orderExists)
				{
					this.TempData[ErrorMessage] = "Поръчка с посочения идентификатор не съществува!";
					return this.RedirectToAction("Index", "Home");
				}

				if (model.FromDate < DateTime.UtcNow || model.ToDate < model.FromDate)
				{
					this.TempData[ErrorMessage] = "Посочената дата е невалидна. Моля променете поръчката!";
					return this.RedirectToAction("Items", "Cart");
				}


				var isReserved = await this.calendarService.ValidateDatesIfItemIsReservedAsync(model.FromDate, model.ToDate, model.OutfitId);

				if (!isReserved)
				{
					await this.orderService.CreateOrderAsync(model, this.User!.GetId()!);
					await this.cartService.DeleteItemFromUserCartAsync(id);
					return this.RedirectToAction("Items", "Cart");
				}
				this.TempData[ErrorMessage] = "Носията вече е заета за посочените дати!";

				return this.RedirectToAction("Items", "Cart");

			}
			catch (Exception)
			{
				return this.GeneralError();
			}

		}

		[HttpPost]
		public async Task<IActionResult> Delete(string orderId, string? baseUrl = null)
		{
			var isAuthenticated = this.User?.Identity?.IsAuthenticated ?? false;

			if (!isAuthenticated)
			{
				return RedirectToAction("All", "OutfitSet");
			}

			try
			{
				var order = await this.orderService
					.ExistsByIdAsync(orderId);

				if (!order)
				{
					this.TempData[ErrorMessage] = "Поръчка с този идентификатор не съществува!";

					return this.RedirectToAction("Mine", "Order");
				}

				if (!this.User!.IsInRole(AdminRoleName))
				{
					bool deleteOnTime = await this.orderService.IsOnTimeAsync(orderId);
					bool userIsOwner = await this.orderService.IsOwnedByTheUserAsync(orderId, this.User!.GetId()!);

					if (!userIsOwner)
					{
						this.TempData[ErrorMessage] = "Нямате права върху тази поръчка!";
						return this.RedirectToAction("Mine", "Order");
					}

					if (!deleteOnTime)
					{
						this.TempData[ErrorMessage] = "Tази поръчка може да бъде премахната само от администратор!";
						return this.RedirectToAction("Mine", "Order");
					}
				}
				await this.orderService.DeleteOrderAsync(orderId);

				this.TempData[WarningMessage] = "Поръчката беше изтрита успешно!";

				if (baseUrl != null)
				{
					return this.Redirect(baseUrl);
				}

				if (this.User!.IsInRole(AdminRoleName))
				{
					return this.RedirectToAction("All", "Order", new { Area = AdminAreaName });
				}

				return this.RedirectToAction("Mine", "Order");
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
