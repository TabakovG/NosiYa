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
		Task<AllEventsPagedServiceModel> AllUnavailableEventsByUserIdAsync(AllEventsPaginatedModel model, string userId); // for All view
		Task<bool> ExistsByIdAsync(int id);
		Task<bool> IsApprovedByIdAsync(int id);
		Task<bool> IsOwnedByUserAsync(int id, string userId);
		Task<EventDetailsViewModel> GetDetailsByIdAsync(int id);
		Task<IEnumerable<EventApprovalViewModel>> GetAllForApproval();



		//Update:

		Task<EventFormModel> GetForEditByIdAsync(int id);
		Task EditByIdAsync(int id, EventFormModel model);
		Task ApproveByIdAsync(int id);

		//Delete:
		Task<EventForDeleteViewModel> GetForDeleteByIdAsync(int id);
		Task DeleteByIdAsync(int id);
	}
}
