﻿namespace NosiYa.Web.Areas.Admin.Controllers
{
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	
	using static Common.SeedingConstants;

	[Area("Admin")]
	[Authorize(Roles = AdminRoleName)]
	[AutoValidateAntiforgeryToken]

	public class BaseAdminController : Controller
	{
	}
}
