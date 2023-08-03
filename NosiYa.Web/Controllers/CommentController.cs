namespace NosiYa.Web.Controllers
{
	using Microsoft.AspNetCore.Mvc;

    using NosiYa.Services.Data.Interfaces;
    using Infrastructure.Extensions;
    using ViewModels.Comment;
	using static Common.NotificationMessagesConstants;


	public class CommentController : BaseController
    {
		private readonly ICommentService commentService;
		public CommentController(ICommentService commentService)
		{
			this.commentService = commentService;
		}
		[HttpPost]
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

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var commentExists = await this.commentService
				.ExistsByIdAsync(id);

			if (!commentExists)
			{
				this.TempData[ErrorMessage] = "Коментар с този идентификатор не съществува!";

				return this.RedirectToAction("All", "Event");
			}

			CommentFormModel comment = await this.commentService
				.GetForEditByIdAsync(id);

			var formModel = new CommentForEditFormModel
            {
				Id = id,
				ModifiedContent = comment.Content,
				EventId = comment.EventId
            };

			return PartialView("_CommentForEditPartial", formModel);
		}

		[HttpPost]
        public async Task<IActionResult> Edit(CommentForEditFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Details", "Event", new { Id = model.EventId });

            }

            var commentExists = await this.commentService
                .ExistsByIdAsync(model.Id);

            if (!commentExists)
            {
                this.TempData[ErrorMessage] = "Коментар с този идентификатор не съществува!";

                return this.RedirectToAction("All", "Event");
            }

            var isAuthenticated = this.User?.Identity?.IsAuthenticated ?? false;

            if (!isAuthenticated)
            {
                return RedirectToAction("Details", "Event", new { Id = model.EventId });
            }

            try
            {
                await this.commentService.EditByModelAsync(model);
                this.TempData[SuccessMessage] = "Промените са запазени успешно и скоро ще бъдат видими! ";
                return RedirectToAction("Details", "Event", new { Id = model.EventId });
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty,
                    "Възникна грешка при обработване на заявката. Моля опитайте отново или се свържете с администратор!");

                return RedirectToAction("Details", "Event", new { Id = model.EventId });
            }
        }
			
        [HttpPost]
        public async Task<IActionResult> Delete(int id, [FromQuery] int eventId)
        {	
			var commentExists = await this.commentService
				.ExistsByIdAsync(id);

			if (!commentExists)
			{
				this.TempData[ErrorMessage] = "Коментар с този идентификатор не съществува!";

				return this.RedirectToAction("All", "Event");
			}

			var isAuthenticated = this.User?.Identity?.IsAuthenticated ?? false;

			if (!isAuthenticated)
			{
				return RedirectToAction("All", "Event");
			}

			try
	        {
		        await this.commentService.DeleteByIdAsync(id);

		        this.TempData[WarningMessage] = "Коментара беше изтрит успешно!";

		        return RedirectToAction("Details", "Event", new { Id = eventId});
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
