namespace NosiYa.Web.Controllers
{
	using Microsoft.AspNetCore.Mvc;

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
	}
}
