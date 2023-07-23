using NosiYa.Services.Data.Interfaces;
using NosiYa.Web.Infrastructure.Extensions;
using NosiYa.Web.ViewModels.Comment;

namespace NosiYa.Web.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using NosiYa.Web.ViewModels.Region;

	public class CommentController : Controller
	{
		private readonly ICommentService commentService;
		public CommentController(ICommentService commentService)
		{
			this.commentService = commentService;
		}

		public async Task<IActionResult> Add(CommentFormModel model)
		{
			try
			{
				//TODO To check if the user is admin

				if (!this.ModelState.IsValid)
				{
					return RedirectToAction("Details", "Event", new { Id = model.EventId });

				}

				var isAuthenticated = this.User?.Identity?.IsAuthenticated ?? false;

				if (!isAuthenticated)
				{
					return RedirectToAction("Details", "Event", new { Id = model.EventId });
				}

				try
				{
					var userId = Guid.Parse(this.User!.GetId()!);
					await this.commentService.CreateCommentAsync(model, userId);

					return this.RedirectToAction("Details", "Event", new { Id = model.EventId });
				}
				catch (Exception)
				{
					return RedirectToAction("Details", "Event", new { Id = model.EventId });
				}
			}
			catch (Exception)
			{
				return this.GeneralError();

			}

		}
		private IActionResult GeneralError()
		{
			this.TempData["ErrorMessage"] =
				"Unexpected error occurred! Please try again later or contact administrator";

			return this.RedirectToAction("Index", "Home");
		}
	}
}
