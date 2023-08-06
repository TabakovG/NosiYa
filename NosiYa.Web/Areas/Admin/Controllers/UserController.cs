
using NosiYa.Services.Data.Models;
using NosiYa.Web.ViewModels.User;

namespace NosiYa.Web.Areas.Admin.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Caching.Memory;
	using Microsoft.AspNetCore.Identity;

	using Data.Models;
	using NosiYa.Services.Data.Interfaces;
	using static Common.ApplicationConstants;
	using static Common.NotificationMessagesConstants;

	public class UserController : BaseAdminController
	{
		private readonly IUserService userService;
		private readonly IMemoryCache memoryCache;
		private readonly UserManager<ApplicationUser> userManager;

		public UserController(
			IUserService userService,
			UserManager<ApplicationUser> userManager,
			ICartService cartService
			)
		{
			this.userService = userService;
			this.userManager = userManager;
		}

		public async Task<IActionResult> All([FromQuery] AllUsersQueryModel queryModel)
		{

			AllUsersFilteredAndPagedServiceModel queryAndSorting = await this.userService.AllAsync(queryModel);

			queryModel.UsersCount = queryAndSorting.TotalUsers;
			queryModel.Users = queryAndSorting.Users;

			return View(queryModel);
		}

		[HttpPost]
		public async Task<IActionResult> DeleteUser(string userId)
		{
			var user = await this.userManager.FindByIdAsync(userId);
			if (user != null)
			{
				try
				{
					await this.userService.DeleteByIdAsync(userId);

					this.memoryCache.Remove(UsersCacheKey);
					this.TempData[SuccessMessage] = "Потребителят беше истрит успешно!";
				}
				catch (Exception)
				{
					this.TempData[ErrorMessage] = "Операцията беше неуспешна!";
				}
			}
			else
			{
				ModelState.AddModelError("", "User Not Found");
				this.TempData[ErrorMessage] = "Потребител с този идентификатор не беше намерен!";

			}

			return RedirectToAction("All");
		}

		[HttpPost]
		public async Task<IActionResult> ConfirmPhone(string userId)
		{
			var user = await userManager.FindByIdAsync(userId);
			if (user != null)
			{
				await this.userService.ConfirmPhoneAsync(userId);

				this.memoryCache.Remove(UsersCacheKey);
				this.TempData[SuccessMessage] = "Телефонът беше потвърден!";

			}
			else
			{
				this.TempData[ErrorMessage] = "Потребител с този идентификатор не беше намерен!";
			}

			return RedirectToAction("All");
		}
	}
}
