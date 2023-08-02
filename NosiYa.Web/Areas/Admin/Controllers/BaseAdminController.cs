using Microsoft.AspNetCore.Authorization;

namespace NosiYa.Web.Areas.Admin.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	[Area("Admin")]
	//[Authorize(Roles = AdminRoleName)]
	public class BaseAdminController : Controller
	{
		
	}
}
