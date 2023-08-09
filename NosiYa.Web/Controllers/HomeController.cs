namespace NosiYa.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Authorization;

    using ViewModels.Home;
    using static Common.SeedingConstants;
    using static Common.ApplicationConstants;

    [AllowAnonymous]
    public class HomeController : BaseController
    {
	    
        public IActionResult Index()
        {
	        if (this.User.IsInRole(AdminRoleName))
	        {
		        return RedirectToAction("Index", "Home", new { Area = AdminAreaName });
	        }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}