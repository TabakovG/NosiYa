namespace NosiYa.Web.Controllers
{
	using Microsoft.AspNetCore.Mvc;

	using Infrastructure.Extensions;
	using NosiYa.Services.Data.Interfaces;
	using ViewModels.Event;

	public class EventController : Controller
	{
		private readonly IEventService eventService;

		public EventController(IEventService eventService)
		{
			this.eventService = eventService;
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


				return View(viewModel);
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
