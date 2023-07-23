using NosiYa.Web.ViewModels.Comment;

namespace NosiYa.Services.Data.Interfaces
{
	public interface ICommentService
	{
		//Create:


		//Read:
		Task<IEnumerable<CommentViewModel>> GetCommentsByEventIdAsync(int eventId);
		
		//Update:


		//Delete:
		Task DeleteByEventIdAsync(int eventId);
		
	}
}
