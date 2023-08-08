namespace NosiYa.Web.Controllers
{
	using System.Collections.Generic;

	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Authorization;

	using Infrastructure.Extensions;
	using NosiYa.Services.Data.Interfaces;
	using ViewModels.Comment;
	using ViewModels.Event;
	using Common;
	using static Common.SeedingConstants;

	[Authorize(Roles = $"{AdminRoleName}, {UserRoleName}")]
	public class EventController : BaseController
	{
		private readonly IEventService eventService;
		private readonly ICommentService commentService;
		private readonly IImageService imageService;
		private readonly IWebHostEnvironment webHostEnvironment;

		public EventController(IEventService eventService, ICommentService commentService, IImageService imageService, IWebHostEnvironment webHostEnvironment)
		{
			this.eventService = eventService;
			this.commentService = commentService;
			this.imageService = imageService;
			this.webHostEnvironment = webHostEnvironment;
		}

		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> All([FromQuery] AllEventsPaginatedModel model)
		{
			var serviceModel = await this.eventService.AllAvailableEventsAsync(model);

			model.Events = serviceModel.Events;
			model.EventsCount = serviceModel.EventsCount;

			return View(model);

		}


		[HttpGet]
		public IActionResult Add()
		{
			try
			{
				var eventModel = new EventFormModel();
				return this.View(eventModel);
			}
			catch (Exception)
			{
				return this.GeneralError();
			}
		}

		[HttpPost]
		public async Task<IActionResult> Add(EventFormModel model, [FromForm] ICollection<IFormFile> elementImages)
		{
			try
			{

				if (!this.ModelState.IsValid)
				{
					return this.View(model);
				}

				var isAuthenticated = this.User?.Identity?.IsAuthenticated ?? false;

				if (!isAuthenticated)
				{
					return this.View(model);
				}

				try
				{
					var userId = Guid.Parse(this.User!.GetId()!);

					//Create event
					int eventId =
						await this.eventService.CreateAndReturnIdAsync(model, userId);

					//Add images to the event
					if (elementImages.Any())
					{
						// Call Add from ImageController without redirecting
						var imageController = new ImageController(imageService, webHostEnvironment);
						imageController.ControllerContext = ControllerContext;

						string entityType = EntityTypesConst.Event;

						// Invoke AddImagesOnEntityCreate Action 
						await imageController.AddImagesOnEntityCreateAsync(eventId, entityType, elementImages);

					}

					return this.RedirectToAction("All", "Event"); //Can't redirect to details as the event must be approved first
				}
				catch (Exception)
				{
					return this.View(model);
				}
			}
			catch (Exception)
			{
				return this.GeneralError();

			}
		}

		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> Details(int id)
		{
			var eventExists = await this.eventService.ExistsByIdAsync(id);

			if (!eventExists)
			{
				this.TempData["ErrorMessage"] = "Събитие с този идентификатор не съществува!";

				return this.RedirectToAction("All", "Event");
			}

			bool eventApproved = await this.eventService.IsApprovedByIdAsync(id);

			var isAuthenticated = this.User?.Identity?.IsAuthenticated ?? false;
			var isAdmin = this.User!.IsInRole(AdminRoleName);

			//Event not approved but user is admin or owner
			if (!eventApproved && isAuthenticated)
			{
				var userIsOwner = await this.eventService.IsOwnedByUserAsync(id, this.User!.GetId()!);

				eventApproved = userIsOwner || isAdmin;
			}

			if (!eventApproved)
			{
				this.TempData["ErrorMessage"] = "Събитие с този идентификатор не съществува!";

				return this.RedirectToAction("All", "Event");
			}

			try
			{
				EventDetailsViewModel viewModel;
				if (isAdmin)
				{
					viewModel = await this.eventService.GetDetailsForAdminByIdAsync(id);
					viewModel.Comments = await this.commentService.GetAllCommentsByEventIdAsync(id);
				}
				else
				{
					viewModel = await this.eventService.GetDetailsByIdAsync(id);
					viewModel.Comments = await this.commentService.GetVisibleCommentsByEventAndUserIdAsync(id, this.User?.GetId() ?? "");
				}
				viewModel.CommentForm = new CommentFormModel()
				{
					EventId = id
				};

				return View(viewModel);
			}
			catch (Exception)
			{
				return this.GeneralError();
			}

		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			try
			{
				var eventExists = await this.eventService.ExistsByIdAsync(id);

				if (!eventExists)
				{
					this.TempData["ErrorMessage"] = "Събитие с този идентификатор не съществува!";

					return this.RedirectToAction("All", "Event");
				}

				var isAuthenticated = this.User?.Identity?.IsAuthenticated ?? false;

				if (!isAuthenticated)
				{
					this.TempData["ErrorMessage"] = "Страницата е достъпна само за оторизирани потребители!";

					return this.RedirectToAction("All", "Event");
				}

				bool userIsOwner = await this.eventService.IsOwnedByUserAsync(id, this.User!.GetId()!);

				if (!userIsOwner && !this.User!.IsInRole(AdminRoleName))
				{
					this.TempData["ErrorMessage"] = "Страницата е достъпна само за оторизирани потребители!";

					return this.RedirectToAction("All", "Event");
				}

				EventFormModel formModel = await this.eventService
					.GetForEditByIdAsync(id);

				//Populate related Images
				formModel.Images = await this.imageService.GetRelatedImagesAsync(id, EntityTypesConst.Event);

				return View(formModel);
			}
			catch (Exception)
			{
				return this.GeneralError();
			}
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, EventFormModel model, [FromForm] ICollection<IFormFile> elementImages)
		{
			if (!this.ModelState.IsValid)
			{
				return this.View(model);
			}

			try
			{
				var eventExists = await this.eventService.ExistsByIdAsync(id);

				if (!eventExists)
				{
					this.TempData["ErrorMessage"] = "Събитие с този идентификатор не съществува!";

					return this.RedirectToAction("All", "Event");
				}

				var isAuthenticated = this.User?.Identity?.IsAuthenticated ?? false;

				if (!isAuthenticated)
				{
					this.TempData["ErrorMessage"] = "Страницата е достъпна само за оторизирани потребители!";

					return this.RedirectToAction("All", "Event");
				}

				bool userIsOwner = await this.eventService.IsOwnedByUserAsync(id, this.User!.GetId()!);

				if (!userIsOwner && !this.User!.IsInRole(AdminRoleName))
				{
					this.TempData["ErrorMessage"] = "Функцията е достъпна само за оторизирани потребители!";

					return this.RedirectToAction("All", "Event");
				}

				await this.eventService.EditByIdAsync(id, model);

				//Add images to the event
				if (elementImages.Count > 0)
				{
					// Call Add from ImageController without redirecting
					var imageController = new ImageController(imageService, webHostEnvironment);
					imageController.ControllerContext = ControllerContext;

					string entityType = EntityTypesConst.Event;

					// Invoke AddImagesOnEntityCreate Action 
					await imageController.UploadImagesAsync(id, entityType, elementImages);

				}

				this.TempData["SuccessMessage"] = "Промените са запазени успешно!";
				return this.RedirectToAction("Details", "Event", new { Id = id });
			}
			catch (Exception)
			{
				this.ModelState.AddModelError(string.Empty,
					"Възникна грешка при обработване на заявката. Моля опитайте отново или се свържете с администратор!");

				return this.View(model);
			}
		}


		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{

			var eventExists = await this.eventService.ExistsByIdAsync(id);

			if (!eventExists)
			{
				this.TempData["ErrorMessage"] = "Събитие с този идентификатор не съществува!";

				return this.RedirectToAction("All", "Event");
			}
			var isAuthenticated = this.User?.Identity?.IsAuthenticated ?? false;

			if (!isAuthenticated)
			{
				this.TempData["ErrorMessage"] = "Страницата е достъпна само за оторизирани потребители!";

				return this.RedirectToAction("All", "Event");
			}

			bool userIsOwner = await this.eventService.IsOwnedByUserAsync(id, this.User!.GetId()!);

			if (!userIsOwner && !this.User!.IsInRole(AdminRoleName))
			{
				this.TempData["ErrorMessage"] = "Функцията е достъпна само за оторизирани потребители!";

				return this.RedirectToAction("All", "Event");
			}
			try
			{
				var viewModel =
					await this.eventService.GetForDeleteByIdAsync(id);

				return this.View(viewModel);
			}
			catch (Exception)
			{
				return this.GeneralError();
			}
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id, EventForDeleteViewModel model)
		{
			var eventExists = await this.eventService.ExistsByIdAsync(id);

			if (!eventExists)
			{
				this.TempData["ErrorMessage"] = "Събитие с този идентификатор не съществува!";

				return this.RedirectToAction("All", "Event");
			}
			var isAuthenticated = this.User?.Identity?.IsAuthenticated ?? false;

			if (!isAuthenticated)
			{
				this.TempData["ErrorMessage"] = "Страницата е достъпна само за оторизирани потребители!";

				return this.RedirectToAction("All", "Event");
			}

			bool userIsOwner = await this.eventService.IsOwnedByUserAsync(id, this.User!.GetId()!);

			if (!userIsOwner && !this.User!.IsInRole(AdminRoleName))
			{
				this.TempData["ErrorMessage"] = "Функцията е достъпна само за оторизирани потребители!";

				return this.RedirectToAction("All", "Event");
			}
			try
			{
				await this.eventService.DeleteByIdAsync(id);
				await this.commentService.DeleteAllByEventIdAsync(id);

				this.TempData["WarningMessage"] = "Събитието беше изтрит успешно!";
				return this.RedirectToAction("All", "Event");
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
