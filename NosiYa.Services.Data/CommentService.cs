namespace NosiYa.Services.Data
{
	using System.Net;

	using Microsoft.EntityFrameworkCore;

	using NosiYa.Data;
	using NosiYa.Data.Models;
	using Interfaces;
	using Web.ViewModels.Comment;
	using static Common.NotificationMessagesConstants;

	public class CommentService : ICommentService
	{
		private readonly NosiYaDbContext context;

		public CommentService(NosiYaDbContext _context)
		{
			this.context = _context;
		}

		public async Task<int> CreateCommentAndReturnIdAsync(CommentFormModel model, Guid userId)
		{
			var comment = new Comment
			{
				Content = WebUtility.HtmlEncode(model.Content),
				ModifiedContent = WebUtility.HtmlEncode(model.Content),
				OwnerId = userId,
				EventId = model.EventId,
				IsActive = true,
				CreatedOn = DateTime.UtcNow
			};

			await context.Comments.AddAsync(comment);
			await context.SaveChangesAsync();

			return comment.Id;
		}

		public async Task<IEnumerable<CommentViewModel>> GetVisibleCommentsByEventAndUserIdAsync(int eventId, string userId)
		{
			var comments = await this.context
				.Comments
				.AsNoTracking()
				.Include(o => o.Owner)
				.Where(c => c.IsActive && c.EventId == eventId)
				.Where(c => c.IsApproved || c.OwnerId.ToString() == userId)
				.OrderByDescending(c => c.CreatedOn)
				.Select(c => new CommentViewModel
				{
					Id = c.Id,
					Content = c.Content,
					OwnerId = c.OwnerId.ToString(),
					OwnerEmail = c.Owner.Email,
					IsWaitingForReview = c.ModifiedContent != null,
					CreatedOn = c.CreatedOn
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
				.Where(c => c.IsActive && c.EventId == eventId)
				.OrderByDescending(c => c.CreatedOn)
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


		public async Task<IEnumerable<CommentApprovalViewModel>> GetAllForApproval()
		{
			return await this.context
				.Comments
				.AsNoTracking()
				.Where(c => c.IsActive)
				.Where(c => c.IsApproved == false || c.ModifiedContent != null)
				.OrderBy(c => c.CreatedOn)
				.Select(e => new CommentApprovalViewModel
				{
					EventName = e.Event.Name,
					EventId = e.EventId,
					Content = e.ModifiedContent ?? e.Content,
					CommentId = e.Id,
					UserName = e.Owner.UserName,
				})
				.ToArrayAsync();
		}

		public async Task<bool> IsOwnedByUserIdAsync(int id, string userId)
		{
			return await this.context
				.Comments
				.Where(c => c.IsActive && c.Id == id)
				.AnyAsync(c => c.OwnerId.ToString() == userId);
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

			comment.ModifiedContent = WebUtility.HtmlEncode(model.ModifiedContent);
			comment.Content = CommentWaitingForReviewText + comment.Content;

			                  await this.context.SaveChangesAsync();
		}

		public async Task ApproveByIdAsync(int id)
		{
			var comment = await this.context
				.Comments
				.Where(e => e.Id == id)
				.FirstAsync();

			comment.IsApproved = true;
			comment.Content = WebUtility.HtmlEncode(comment.ModifiedContent ?? comment.Content)
				.Replace(CommentWaitingForReviewText, string.Empty);
			comment.ModifiedContent = null;

			await this.context.SaveChangesAsync();
		}

		public async Task DeleteAllByEventIdAsync(int eventId)
		{
			var comments = await this.context
				.Comments
				.Where(c => c.IsActive && c.EventId == eventId)
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
