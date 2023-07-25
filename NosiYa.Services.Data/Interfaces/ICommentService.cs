using NosiYa.Web.ViewModels.Comment;

namespace NosiYa.Services.Data.Interfaces
{
	public interface ICommentService
	{
		//Create:
		Task CreateCommentAsync(CommentFormModel model, Guid userId);

		//Read:
		Task<IEnumerable<CommentViewModel>> GetCommentsByEventIdAsync(int eventId);
		Task<CommentFormModel> GetForEditByIdAsync(int id);
		Task<bool> ExistsByIdAsync(int id);

		//Update:
		Task EditByModelAsync(CommentForEditFormModel model);

		//Delete:
		Task DeleteAllByEventIdAsync(int eventId);
		Task DeleteByIdAsync(int id);

	}
}
