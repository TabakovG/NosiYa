namespace NosiYa.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using static Common.SeedingConstants;   

	[Authorize(Roles = $"{UserRoleName}, {AdminRoleName}")]
    public class BaseController : Controller
    {

    }
}
