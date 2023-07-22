﻿using NosiYa.Web.ViewModels.Event;

namespace NosiYa.Services.Data.Interfaces
{
	using NosiYa.Services.Data.Model;
	using NosiYa.Web.ViewModels.Region;

	public interface IEventService
	{
		//Create:

		Task<int> CreateAndReturnIdAsync(EventFormModel model);

		//Read:

		Task<AllEventsPagedServiceModel> AllAvailableEventsAsync(AllEventsPaginatedModel model); // for All view

		Task<bool> ExistsByIdAsync(int id);

		Task<EventDetailsViewModel> GetDetailsByIdAsync(int id);


		//Update:

		Task<EventFormModel> GetForEditByIdAsync(int id);
		Task EditByIdAsync(int id, EventFormModel model);


		//Delete:

		Task DeleteByIdAsync(int id);
	}
}