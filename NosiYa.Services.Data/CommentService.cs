using Microsoft.EntityFrameworkCore;
using NosiYa.Data;
using NosiYa.Data.Models;
using NosiYa.Services.Data.Interfaces;
using NosiYa.Web.ViewModels;
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

		public async Task CreateCommentAsync(CommentFormModel model, Guid userId)
		{
			var comment = new Comment
			{
				Content = model.Content,
				OwnerId = userId,
				EventId = model.EventId,
			};

			await context.Comments.AddAsync(comment);
			await context.SaveChangesAsync();
		}

		public async Task<IEnumerable<CommentViewModel>> GetVisibleCommentsByEventAndUserIdAsync(int eventId, string userId)
		{
			var comments = await this.context
				.Comments
				.AsNoTracking()
				.Include(o=>o.Owner)
				.Where(c => c.IsActive
				            && c.IsApproved || c.OwnerId.ToString() == userId
				            && c.EventId == eventId)
				.Select(c => new CommentViewModel
				{
					Id = c.Id,
					Content = c.Content,
					OwnerId = c.OwnerId.ToString(),
					OwnerEmail = c.Owner.Email,
					IsWaitingForReview = c.ModifiedContent != null
                })
				.ToArrayAsync();

			return comments;
		}

		public async Task<IEnumerable<CommentViewModel>> GetAllCommentsByEventIdAsync(int eventId)
		{
			var comments = await this.context
				.Comments
				.AsNoTracking()
				.Include(o => o.Owner)
				.Where(c => c.IsActive
				            && c.EventId == eventId)
				.Select(c => new CommentViewModel
				{
					Id = c.Id,
					Content = c.Content,
					OwnerId = c.OwnerId.ToString(),
					OwnerEmail = c.Owner.Email,
					IsWaitingForReview = c.ModifiedContent != null
				})
				.ToArrayAsync();

			return comments;
		}

		public async Task<bool> ExistsByIdAsync(int id)
		{
			return await this.context
				.Comments
				.AsNoTracking()
				.AnyAsync(c => c.IsActive && c.Id == id);
		}

		public async Task<bool> IsApprovedByIdAsync(int id)
		{
			return await this.context
				.Comments
				.AsNoTracking()
				.AnyAsync(c => c.IsApproved && c.Id == id);
		}

		public async Task<IEnumerable<ApprovalViewModel>> GetAllForApproval()
		{
			return await this.context
				.Comments
				.AsNoTracking()
				.Where(c => c.IsApproved == false && c.IsActive)
				.Select(e => new ApprovalViewModel
				{
					DetailsPath = "/Event/Details/",
					Element = e.ModifiedContent ?? e.Content,
					ElementId = e.EventId.ToString(),
					UserName = e.Owner.UserName,
				})
				.ToArrayAsync();
		}

		public async Task<CommentFormModel> GetForEditByIdAsync(int id)
		{
			var comment = await this.context
				.Comments
				.Where(c => c.IsActive && c.Id == id)
				.FirstAsync();

			return new CommentFormModel
			{
				Content = comment.ModifiedContent ?? comment.Content,
				EventId = comment.EventId,
			};

		}

		public async Task EditByModelAsync(CommentForEditFormModel model)
		{
			var comment = await this.context
				.Comments
				.Where(c => c.IsActive && c.Id == model.Id)
				.FirstAsync();

			comment.ModifiedContent = model.ModifiedContent;

			await this.context.SaveChangesAsync();
		}

		public async Task ApproveByIdAsync(int id)
		{
			var comment = await this.context
				.Comments
				.Where(e => e.Id == id)
				.FirstAsync();

			comment.IsApproved = true;
			await this.context.SaveChangesAsync();
		}

		public async Task DeleteAllByEventIdAsync(int eventId)
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

		public async Task DeleteByIdAsync(int id)
		{
			var comment = await this.context
				.Comments
				.Where(c => c.IsActive && c.Id == id)
				.FirstAsync();

			comment.IsActive = false;
			await this.context.SaveChangesAsync();
		}
	}
}
