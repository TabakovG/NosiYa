namespace NosiYa.Web.Areas.Admin.Controllers
{
	using Microsoft.AspNetCore.Mvc;

	using NosiYa.Services.Data.Interfaces;
	using NosiYa.Web.ViewModels.Comment;
	using NosiYa.Web.ViewModels.Event;
	using static Common.NotificationMessagesConstants;

	public class EventController : BaseAdminController
	{
		private readonly IEventService eventService;
		private readonly ICommentService commentService;


		public EventController(IEventService eventService, ICommentService commentService)
		{
			this.eventService = eventService;
			this.commentService = commentService;

		}

		[HttpPost]
		public async Task<IActionResult> Approve([FromForm] int elementId)
		{
			try
			{
				var eventExists = await this.eventService.ExistsByIdAsync(elementId);

				if (!eventExists)
				{
					this.TempData["ErrorMessage"] = "Събитие с този идентификатор не съществува!";

					return this.RedirectToAction("Approvals", "Home");
				}

				await this.eventService.ApproveByIdAsync(elementId);

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
