﻿namespace NosiYa.Web.Controllers
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

			if (!this.ModelState.IsValid)
			{
				return RedirectToAction("Details", "Event", new { Id = model.EventId });
			}

			try
			{

				var userId = Guid.Parse(this.User!.GetId()!);
				var commentId = await this.commentService.CreateCommentAndReturnIdAsync(model, userId);

				if (this.User.IsAdmin())
				{
					await this.commentService.ApproveByIdAsync(commentId);
				}
				this.TempData[SuccessMessage] = "Коментара е добавен успешно и скоро ще бъде видим!";

				return this.RedirectToAction("Details", "Event", new { Id = model.EventId });

			}
			catch (Exception)
			{
				return RedirectToAction("Details", "Event", new { Id = model.EventId });
			}
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			try
			{
				var commentExists = await this.commentService
				.ExistsByIdAsync(id);

				if (!commentExists)
				{
					this.TempData[ErrorMessage] = "Коментар с този идентификатор не съществува!";

					return this.RedirectToAction("All", "Event");
				}

				var userIsOwner = await this.commentService.IsOwnedByUserIdAsync(id, this.User!.GetId()!);

				if (!userIsOwner && !this.User.IsAdmin())
				{
					this.TempData[ErrorMessage] = "Нямате права върху този коментар!";

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
			catch (Exception)
			{
				return this.GeneralError();
			}
		}

		[HttpPost]
		public async Task<IActionResult> Edit(CommentForEditFormModel model)
		{
			if (!this.ModelState.IsValid)
			{
				return Json(new { success = false });
			}

			try
			{
				var commentExists = await this.commentService
				.ExistsByIdAsync(model.Id);

				if (!commentExists)
				{
					this.TempData[ErrorMessage] = "Коментар с този идентификатор не съществува!";
					return Json(new { success = false });
				}

				var userIsOwner = await this.commentService.IsOwnedByUserIdAsync(model.Id, this.User!.GetId()!);

				if (!userIsOwner && !this.User.IsAdmin())
				{
					this.TempData[ErrorMessage] = "Нямате права върху този коментар!";
					return Json(new { success = false });
				}

				await this.commentService.EditByModelAsync(model);

				if (this.User.IsAdmin())
				{
					await this.commentService.ApproveByIdAsync(model.Id);
					return Json(new { success = true, editedCommentContent = model.ModifiedContent });

				}
				this.TempData[SuccessMessage] = "Промените са запазени успешно и скоро ще бъдат видими! ";
				return Json(new { success = true, editedCommentContent = CommentWaitingForReviewText });
			}
			catch (Exception)
			{
				this.ModelState.AddModelError(string.Empty,
					"Възникна грешка при обработване на заявката. Моля опитайте отново или се свържете с администратор!");

				return Json(new { success = false });

			}
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id, [FromQuery] int eventId, string? baseUrl = null)
		{
			try
			{
				var commentExists = await this.commentService
				.ExistsByIdAsync(id);

				if (!commentExists)
				{
					this.TempData[ErrorMessage] = "Коментар с този идентификатор не съществува!";

					return this.RedirectToAction("All", "Event");
				}

				var userIsOwner = await this.commentService.IsOwnedByUserIdAsync(id, this.User!.GetId()!);

				if (!userIsOwner && !this.User.IsAdmin())
				{
					this.TempData[ErrorMessage] = "Нямате права върху този коментар!";

					return RedirectToAction("Details", "Event", new { Id = eventId });
				}

				await this.commentService.DeleteByIdAsync(id);

				this.TempData[WarningMessage] = "Коментара беше изтрит успешно!";

				if (baseUrl != null)
				{
					return this.Redirect(baseUrl);
				}

				return RedirectToAction("Details", "Event", new { Id = eventId });
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
