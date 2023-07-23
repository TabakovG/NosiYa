namespace NosiYa.Web.Controllers
{
	using Microsoft.AspNetCore.Mvc;

	public class CommentController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
