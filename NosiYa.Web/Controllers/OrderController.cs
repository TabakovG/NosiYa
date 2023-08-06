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

			ICollection<OrderViewModel> orderModel = await this.orderService.GetOrdersByUserIdAsync(this.User!.GetId()!);

			return View(orderModel);
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

			var orderExists = await this.cartService.CartItemExistsById(id);
			if (!orderExists)
			{
				this.TempData[ErrorMessage] = "Поръчка с посочения идентификатор не съществува!";
				return this.RedirectToAction("Index", "Home");
			}

			//TODO validate dates

			try
			{
				var isReserved = await this.calendarService.ValidateDatesAsync(model.FromDate, model.ToDate, model.OutfitId);

				if (!isReserved)
				{
					await this.orderService.CreateOrderAsync(model, this.User!.GetId()!);
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

		public async Task<IActionResult> Delete(string orderId)
		{
			var isAuthenticated = this.User?.Identity?.IsAuthenticated ?? false;

			if (!isAuthenticated)
			{
				return RedirectToAction("All", "OutfitSet");
			}
			
			var order = await this.orderService
				.ExistsByIdAsync(orderId);

			if (!order)
			{
				this.TempData[ErrorMessage] = "Поръчка с този идентификатор не съществува!";

				return this.RedirectToAction("Mine", "Order");
			}

			try
			{
				await this.orderService.DeleteOrderAsync(orderId);

				this.TempData[WarningMessage] = "Поръчката беше изтрита успешно!";

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
