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
		
		//Update:
		Task EditByIdAsync(int id, CommentFormModel model);

		//Delete:
		Task DeleteByEventIdAsync(int eventId);
		
	}
}
