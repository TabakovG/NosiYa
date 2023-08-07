namespace NosiYa.Services.Data.Interfaces
{
	using Models;
	using NosiYa.Web.ViewModels;
	using Web.ViewModels.Event;

	public interface IEventService
	{
		//Create:

		Task<int> CreateAndReturnIdAsync(EventFormModel model, Guid userId);

		//Read:

		Task<AllEventsPagedServiceModel> AllAvailableEventsAsync(AllEventsPaginatedModel model); // for All view
		Task<bool> ExistsByIdAsync(int id);
		Task<bool> IsApprovedByIdAsync(int id);
		Task<bool> IsOwnedByUserAsync(int id, string userId);
		Task<EventDetailsViewModel> GetDetailsByIdAsync(int id);
		Task<EventDetailsViewModel> GetDetailsForAdminByIdAsync(int id);
		Task<IEnumerable<ApprovalViewModel>> GetAllForApproval();



		//Update:

		Task<EventFormModel> GetForEditByIdAsync(int id);
		Task EditByIdAsync(int id, EventFormModel model);
		Task ApproveByIdAsync(int id);

		//Delete:
		Task<EventForDeleteViewModel> GetForDeleteByIdAsync(int id);
		Task DeleteByIdAsync(int id);
	}
}
