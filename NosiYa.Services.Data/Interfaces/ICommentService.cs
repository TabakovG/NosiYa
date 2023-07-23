using NosiYa.Web.ViewModels.Comment;

namespace NosiYa.Services.Data.Interfaces
{
	public interface ICommentService
	{
		Task<IEnumerable<CommentViewModel>> GetCommentsByEventIdAsync(int eventId);
	}
}
