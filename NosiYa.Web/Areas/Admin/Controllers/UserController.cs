
namespace NosiYa.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.AspNetCore.Identity;

    using Data.Models;
    using ViewModels.User;
    using NosiYa.Services.Data.Interfaces;
    using static Common.ApplicationConstants;
    using static Common.NotificationMessagesConstants;

    public class UserController : BaseAdminController
    {
        private readonly IUserService userService;
        private readonly IMemoryCache memoryCache;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public UserController(
            IUserService userService,
            IMemoryCache memoryCache,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager
            )
        {
            this.userService = userService;
            this.memoryCache = memoryCache;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Client)]
        public async Task<IActionResult> All()
        {
            IEnumerable<UserViewModel> users =
                this.memoryCache.Get<IEnumerable<UserViewModel>>(UsersCacheKey);
            if (users == null)
            {
                users = await this.userService.AllAsync();

                MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan
                        .FromMinutes(UsersCacheDurationMinutes));

                this.memoryCache.Set(UsersCacheKey, users, cacheOptions);

            }

            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    this.memoryCache.Remove(UsersCacheKey);
                    this.TempData[SuccessMessage] = "Потребителят беше истрит успешно!";

                }
                else
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
