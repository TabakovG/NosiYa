namespace NosiYa.Services.Data.Interfaces
{
	using NosiYa.Web.ViewModels;
	using Web.ViewModels.Comment;

	public interface ICommentService
	{
		//Create:
		Task CreateCommentAsync(CommentFormModel model, Guid userId);

		//Read:
		Task<IEnumerable<CommentViewModel>> GetVisibleCommentsByEventAndUserIdAsync(int eventId, string userId);
		Task<IEnumerable<CommentViewModel>> GetAllCommentsByEventIdAsync(int eventId); //Admin only
		Task<CommentFormModel> GetForEditByIdAsync(int id);
		Task<bool> ExistsByIdAsync(int id);
		Task<IEnumerable<CommentApprovalViewModel>> GetAllForApproval(); //Admin only
		Task<bool> IsOwnedByUserIdAsync(int id, string userId);


		//Update:
		Task EditByModelAsync(CommentForEditFormModel model);
		Task ApproveByIdAsync(int id); //Admin only

		//Delete:
		Task DeleteAllByEventIdAsync(int eventId);
		Task DeleteByIdAsync(int id);

	}
}
