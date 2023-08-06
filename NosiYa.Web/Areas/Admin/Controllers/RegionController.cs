namespace NosiYa.Web.Areas.Admin.Controllers
{
	using Microsoft.AspNetCore.Mvc;

	public class RegionController : BaseAdminController
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
