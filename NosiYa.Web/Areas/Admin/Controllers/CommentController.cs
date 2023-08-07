namespace NosiYa.Web.Areas.Admin.Controllers
{
	using Microsoft.AspNetCore.Mvc;

	using NosiYa.Services.Data.Interfaces;
	using static Common.NotificationMessagesConstants;

	public class CommentController : BaseAdminController
	{
		private readonly ICommentService commentService;

		public CommentController(ICommentService commentService)
		{
			this.commentService = commentService;
		}

		[HttpPost]
		public async Task<IActionResult> Approve([FromForm] int elementId)
		{
			try
			{
				var commentExist = await this.commentService.ExistsByIdAsync(elementId);

				if (!commentExist)
				{
					this.TempData["ErrorMessage"] = "Коментар с този идентификатор не съществува!";

					return this.RedirectToAction("Approvals", "Home");
				}

				await this.commentService.ApproveByIdAsync(elementId);

				return this.RedirectToAction("Approvals", "Home");

			}
			catch (Exception)
			{
				return this.GeneralError();
			}
		}
		private IActionResult GeneralError()
		{
			this.TempData[ErrorMessage] =
				"Unexpected error occurred! Please try again later or contact administrator";

			return this.RedirectToAction("Index", "Home");
		}
	}
}
