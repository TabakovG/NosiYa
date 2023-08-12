namespace NosiYa.Web.Areas.Admin.Controllers
{
	using Microsoft.AspNetCore.Mvc;

	using NosiYa.Services.Data.Interfaces;
	using NosiYa.Web.ViewModels.Order;
	using NosiYa.Services.Messaging;
	using static Common.NotificationMessagesConstants;

	public class OrderController : BaseAdminController
	{
		private readonly IOrderService orderService;
		private readonly IEmailSender emailSender;
		private readonly IUserService userService;



		public OrderController(IOrderService orderService, IEmailSender emailSender, IUserService userService)
		{
			this.orderService = orderService;
			this.emailSender = emailSender;
			this.userService = userService;
		}

		[HttpGet]
		public IActionResult All()
		{
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> Details(string id)
		{
			try
			{
				var orderExists = await this.orderService.ExistsByIdAsync(id);

				if (!orderExists)
				{
					this.TempData[ErrorMessage] = "Поръчката не съществува!";

					return this.RedirectToAction("All");
				}

				OrderDetailsViewModel viewModel = await this.orderService
					.GetOrderDetailsByIdAsync(id);

				return View(viewModel);
			}
			catch (Exception)
			{
				return this.GeneralError();
			}
		}

		[HttpPost]
		public async Task<IActionResult> Approve([FromForm] string elementId, [FromForm] string email, [FromForm] string outfitSet)
		{
			try
			{
				var orderExists = await this.orderService.ExistsByIdAsync(elementId);

				if (!orderExists)
				{
					this.TempData[ErrorMessage] = "Поръчката не съществува!";

					return this.RedirectToAction("All");
				}

				await this.orderService.ApproveByIdAsync(elementId);

				var emailExist = await this.userService.UserExistByEmail(email);
				if (emailExist)
				{
					await emailSender.SendEmailAsync(
						email,
						"Одобрена поръчка",
						$"Здравейте, Заявката ви за наем на {outfitSet} беше одобрена.");
				}

				return this.RedirectToAction("Approvals", "Home");

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
