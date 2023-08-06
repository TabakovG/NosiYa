namespace NosiYa.Services.Data.Interfaces
{
	using NosiYa.Web.ViewModels;
	using Web.ViewModels.Comment;

	public interface ICommentService
	{
		//Create:
		Task CreateCommentAsync(CommentFormModel model, Guid userId);

		//Read:
		Task<IEnumerable<CommentViewModel>> GetCommentsByEventIdAsync(int eventId);
		Task<CommentFormModel> GetForEditByIdAsync(int id);
		Task<bool> ExistsByIdAsync(int id);
		Task<bool> ApprovedByIdAsync(int id);
		Task<IEnumerable<ApprovalViewModel>> GetAllForApproval();


		//Update:
		Task EditByModelAsync(CommentForEditFormModel model);

		//Delete:
		Task DeleteAllByEventIdAsync(int eventId);
		Task DeleteByIdAsync(int id);

	}
}
