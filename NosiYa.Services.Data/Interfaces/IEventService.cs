namespace NosiYa.Services.Data.Interfaces
{
	using Models;
	using Web.ViewModels.Event;

	public interface IEventService
	{
		//Create:

		Task<int> CreateAndReturnIdAsync(EventFormModel model, Guid userId);

		//Read:

		Task<AllEventsPagedServiceModel> AllAvailableEventsAsync(AllEventsPaginatedModel model); // for All view

		Task<bool> ExistsByIdAsync(int id);

		Task<EventDetailsViewModel> GetDetailsByIdAsync(int id);


		//Update:

		Task<EventFormModel> GetForEditByIdAsync(int id);
		Task EditByIdAsync(int id, EventFormModel model);


		//Delete:
		Task<EventForDeleteViewModel> GetForDeleteByIdAsync(int id);
		Task DeleteByIdAsync(int id);
	}
}
