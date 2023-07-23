using NosiYa.Web.ViewModels.Comment;

namespace NosiYa.Web.Controllers
{
	using Microsoft.AspNetCore.Mvc;

	using Infrastructure.Extensions;
	using NosiYa.Services.Data.Interfaces;
	using ViewModels.Event;

	public class EventController : Controller
	{
		private readonly IEventService eventService;
		private readonly ICommentService commentService;

		public EventController(IEventService eventService, ICommentService commentService)
		{
			this.eventService = eventService;
			this.commentService = commentService;
		}

		public async Task<IActionResult> All([FromQuery] AllEventsPaginatedModel model)
		{
			var serviceModel = await this.eventService.AllAvailableEventsAsync(model);

			model.Events = serviceModel.Events;
			model.EventsCount = serviceModel.EventsCount;

			return View(model);

		}


		[HttpGet]
		public async Task<IActionResult> Add()
		{
			try
			{
				var  eventModel = new EventFormModel();
				return this.View(eventModel);
			}
			catch (Exception)
			{
				return this.GeneralError();
			}
		}

		[HttpPost]
		public async Task<IActionResult> Add(EventFormModel model)
		{
			try
			{
				//TODO To check if the user is admin

				if (!this.ModelState.IsValid)
				{
					return this.View(model);
				}

				var isAuthenticated = this.User?.Identity?.IsAuthenticated ?? false;

				if (!isAuthenticated)
				{
					return this.View(model);
				}

				try
				{
					var userId = Guid.Parse(this.User!.GetId()!);

					int eventId =
						await this.eventService.CreateAndReturnIdAsync(model, userId);

					return this.RedirectToAction("Details", "Event", new { Id = eventId });
				}
				catch (Exception)
				{
					return this.View(model);
				}
			}
			catch (Exception)
			{
				return this.GeneralError();

			}
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
					.GetDetailsByIdAsync(id);
				viewModel.Comments = await this.commentService.GetCommentsByEventIdAsync(id);
				viewModel.CommentForm = new CommentFormModel()
				{
					EventId = id
				};

				return View(viewModel);
			}
			catch (Exception)
			{
				return this.GeneralError();
			}

		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var eventExists = await this.eventService.ExistsByIdAsync(id);

			if (!eventExists)
			{
				this.TempData["ErrorMessage"] = "Събитие с този идентификатор не съществува!";

				return this.RedirectToAction("All", "Event");
			}

			try
			{
				EventFormModel formModel = await this.eventService
					.GetForEditByIdAsync(id);

				return View(formModel);
			}
			catch (Exception)
			{
				return this.GeneralError();
			}
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, EventFormModel model)
		{
			if (!this.ModelState.IsValid)
			{
				return this.View(model);
			}

			var eventExists = await this.eventService.ExistsByIdAsync(id);

			if (!eventExists)
			{
				this.TempData["ErrorMessage"] = "Събитие с този идентификатор не съществува!";

				return this.RedirectToAction("All", "Event");
			}

			var isAuthenticated = this.User?.Identity?.IsAuthenticated ?? false;

			if (!isAuthenticated)
			{
				return this.View(model);
			}

			try
			{
				await this.eventService.EditByIdAsync(id, model);
				this.TempData["SuccessMessage"] = "Промените са запазени успешно!";
				return this.RedirectToAction("Details", "Event", new { Id = id });
			}
			catch (Exception)
			{
				this.ModelState.AddModelError(string.Empty,
					"Възникна грешка при обработване на заявката. Моля опитайте отново или се свържете с администратор!");

				return this.View(model);
			}
		}


		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{

			var eventExists = await this.eventService.ExistsByIdAsync(id);

			if (!eventExists)
			{
				this.TempData["ErrorMessage"] = "Събитие с този идентификатор не съществува!";

				return this.RedirectToAction("All", "Event");
			}

			try
			{
				var viewModel =
					await this.eventService.GetForDeleteByIdAsync(id);

				return this.View(viewModel);
			}
			catch (Exception)
			{
				return this.GeneralError();
			}
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id, EventForDeleteViewModel model)
		{
			var eventExists = await this.eventService.ExistsByIdAsync(id);

			if (!eventExists)
			{
				this.TempData["ErrorMessage"] = "Събитие с този идентификатор не съществува!";

				return this.RedirectToAction("All", "Event");
			}

			try
			{
				await this.eventService.DeleteByIdAsync(id);
				await this.commentService.DeleteByEventIdAsync(id);

				this.TempData["WarningMessage"] = "Събитието беше изтрит успешно!";
				return this.RedirectToAction("All", "Event");
			}
			catch (Exception)
			{
				return this.GeneralError();
			}
		}


		private IActionResult GeneralError()
		{
			this.TempData["ErrorMessage"] =
				"Unexpected error occurred! Please try again later or contact administrator";

			return this.RedirectToAction("Index", "Home");
		}

	}
}
