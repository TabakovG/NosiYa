namespace NosiYa.Web.Areas.Admin.Controllers
{
	using Microsoft.AspNetCore.Mvc;

	using NosiYa.Services.Data.Interfaces;
	using ViewModels.Region;
	using static Common.NotificationMessagesConstants;

	public class OrderController : BaseAdminController
	{
		private readonly IOrderService orderService;


		public OrderController(IOrderService orderService)
		{
			this.orderService = orderService;
		}

		[HttpGet]
		public IActionResult All()
		{

			return View();
		}

		[HttpGet]
		public async Task<IActionResult> Details(string id)
		{
			var orderExists = await this.orderService.ExistsByIdAsync(id);

			if (!orderExists)
			{
				this.TempData[ErrorMessage] = "Поръчката не съществува!";

				return this.RedirectToAction("All");
			}

			try
			{
				var viewModel = await this.orderService
					.GetOrderByIdAsync(id);


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
