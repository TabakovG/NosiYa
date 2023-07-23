using Microsoft.EntityFrameworkCore;
using NosiYa.Data;
using NosiYa.Services.Data.Interfaces;
using NosiYa.Web.ViewModels.Comment;

namespace NosiYa.Services.Data
{
	public class CommentService : ICommentService
	{
		private readonly NosiYaDbContext context;

		public CommentService(NosiYaDbContext _context)
		{
			this.context = _context;
		}
		public async Task<IEnumerable<CommentViewModel>> GetCommentsByEventIdAsync(int eventId)
		{
			var comments = await this.context
				.Comments
				.AsNoTracking()
				.Include(o=>o.Owner)
				.Where(c => c.IsActive
				            && c.IsApproved
				            && c.EventId == eventId)
				.Select(c => new CommentViewModel
				{
					Id = c.Id,
					Content = c.Content,
					OwnerId = c.OwnerId.ToString(),
					OwnerEmail = c.Owner.Email
				})
				.ToArrayAsync();

			return comments;
		}

		public async Task DeleteByEventIdAsync(int eventId)
		{
			var comments = await this.context
				.Comments
				.Where(c=>c.IsActive && c.EventId == eventId)
				.ToArrayAsync();

			foreach (var comment in comments)
			{
				comment.IsActive = false;
			}

			await this.context.SaveChangesAsync();
		}
	}
}
