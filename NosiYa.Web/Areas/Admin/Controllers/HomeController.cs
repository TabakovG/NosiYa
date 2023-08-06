using NosiYa.Services.Data.Interfaces;

namespace NosiYa.Web.Areas.Admin.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using ViewModels;

	public class HomeController : BaseAdminController
	{
		private readonly IOrderService orderService;
		private readonly IEventService eventService;
		private readonly ICommentService commentService;

		public HomeController(IOrderService orderService, IEventService eventService, ICommentService commentService)
		{
			this.orderService = orderService;
			this.eventService = eventService;
			this.commentService = commentService;
		}

		public IActionResult Index()
		{
			return View();
		}

        public async Task<IActionResult> Approvals()
        {
	        try
	        {
		        var model = new AdminApprovalViewModel
		        {
			        Rents = await this.orderService.GetAllForApproval(),
			        Events = await this.eventService.GetAllForApproval(),
			        Comments = await this.commentService.GetAllForApproval()
		        };
			return View(model);
			}
	        catch (Exception)
	        {
		        return this.RedirectToAction("Index");
	        }
	        

        }
	}
}
