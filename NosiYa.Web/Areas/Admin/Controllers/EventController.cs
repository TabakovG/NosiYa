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

		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			var eventExists = await this.eventService.ExistsByIdAsync(id);

			if (!eventExists)
			{
				this.TempData["ErrorMessage"] = "Събитие с този идентификатор не съществува!";

				return this.RedirectToAction("All", "Event");
			}

			try
			{
				EventDetailsViewModel viewModel = await this.eventService
					.GetDetailsForAdminByIdAsync(id);

				viewModel.Comments = await this.commentService.GetAllCommentsByEventIdAsync(id);
				viewModel.CommentForm = new CommentFormModel()
				{
					EventId = id
				};

				return View("_EventDetailsView", viewModel);
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
