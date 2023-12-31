﻿namespace NosiYa.Web.Areas.Admin.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using Common;
	using NosiYa.Services.Data.Interfaces;
	using NosiYa.Web.Controllers;
	using NosiYa.Web.ViewModels.OutfitPart;
	using NosiYa.Web.ViewModels.OutfitSet;
	using static Common.NotificationMessagesConstants;

	public class OutfitPartController : BaseAdminController
	{
		private readonly IOutfitPartService outfitPartService;
		private readonly IOutfitSetService outfitSetService;
		private readonly IUserService userService;
		private readonly IImageService imageService;
		private readonly IWebHostEnvironment webHostEnvironment;

		public OutfitPartController(IOutfitPartService outfitPartService, IOutfitSetService outfitSetService, IUserService userService, IImageService imageService, IWebHostEnvironment webHostEnvironment)
		{
			this.outfitPartService = outfitPartService;
			this.outfitSetService = outfitSetService;
			this.userService = userService;
			this.imageService = imageService;
			this.webHostEnvironment = webHostEnvironment;
		}

		[HttpGet]
		public async Task<IActionResult> Add(int id)
		{
			try
			{
				var outfitPart = new OutfitPartFormModel()
				{
					OutfitSets = await this.outfitSetService.GetOutfitSetsAsOptionsAsync(id),
					OutfitSetId = id
				};
				return this.View(outfitPart);
			}
			catch (Exception)
			{
				return this.GeneralError();
			}
		}

		[HttpPost]
		public async Task<IActionResult> Add(OutfitPartFormModel model, [FromForm] ICollection<IFormFile> elementImages)
		{
			if (!this.ModelState.IsValid)
			{
				model.OutfitSets = await this.outfitSetService.GetOutfitSetsAsOptionsAsync(model.OutfitSetId);
				return this.View(model);
			}

			try
			{
				var ownerExist = await this.userService.UserExistByEmail(model.OwnerEmail);

				if (!ownerExist)
				{
					model.OutfitSets = await this.outfitSetService.GetOutfitSetsAsOptionsAsync(model.OutfitSetId);
					this.TempData[ErrorMessage] = "Посочения собственик на елемента няма акаунт в системата!";
					return this.View(model);
				}

				model.OwnerId = await this.userService.GetUserIdFromEmailAsync(model.OwnerEmail);


				int outfitPartId =
					await this.outfitPartService.CreateAndReturnIdAsync(model);

				//Add images to the event
				if (elementImages.Any())
				{
					// Call Add from ImageController without redirecting
					var imageController = new ImageController(imageService, webHostEnvironment);
					imageController.ControllerContext = ControllerContext;

					string entityType = EntityTypesConst.OutfitPart;

					// Invoke AddImagesOnEntityCreate Action 
					await imageController.AddImagesOnEntityCreateAsync(outfitPartId, entityType, elementImages);

				}

				return this.RedirectToAction("Details", "OutfitSet", new { Area = "", Id = model.OutfitSetId });
			}
			catch (Exception)
			{
				model.OutfitSets = await this.outfitSetService.GetOutfitSetsAsOptionsAsync(model.OutfitSetId);
				return this.View(model);
			}
		}


		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			try
			{
				var outfitPartExists = await this.outfitPartService.ExistByIdAsync(id);

				if (!outfitPartExists)
				{
					this.TempData[ErrorMessage] = "Елемент на носия с този идентификатор не съществува!";

					return this.RedirectToAction("All", "OutfitSet", new { Area = "" });
				}

				OutfitPartFormModel formModel = await this.outfitPartService
					.GetForEditByIdAsync(id);

				//Populate OutfitSet Options
				formModel.OutfitSets = await this.outfitSetService.GetOutfitSetsAsOptionsAsync();

				//Populate related Images
				formModel.Images = await this.imageService.GetRelatedImagesAsync(id, EntityTypesConst.OutfitPart);

				return View(formModel);
			}
			catch (Exception)
			{
				return this.GeneralError();
			}
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, OutfitPartFormModel model, [FromForm] ICollection<IFormFile> elementImages)
		{
			if (!this.ModelState.IsValid)
			{
				model.OutfitSets = await this.outfitSetService.GetOutfitSetsAsOptionsAsync();
				return this.View(model);
			}

			try
			{
				var outfitPartExists = await this.outfitPartService.ExistByIdAsync(id);

				if (!outfitPartExists)
				{
					this.TempData[ErrorMessage] = "Елемент на носия с този идентификатор не съществува!";

					return this.RedirectToAction("All", "OutfitSet", new { Area = "" });
				}

				var ownerExist = await this.userService.UserExistByEmail(model.OwnerEmail);

				if (!ownerExist)
				{
					model.OutfitSets = await this.outfitSetService.GetOutfitSetsAsOptionsAsync();
					this.TempData[ErrorMessage] = "Посочения собственик на елемента няма акаунт в системата!";
					return this.View(model);
				}

				model.OwnerId = await this.userService.GetUserIdFromEmailAsync(model.OwnerEmail);

				var outfitSetType = await this.outfitSetService.GetRenterTypeByIdAsync(model.OutfitSetId);
				if (outfitSetType != model.RenterType)
				{
					model.OutfitSets = await this.outfitSetService.GetOutfitSetsAsOptionsAsync();
					this.TempData[ErrorMessage] = $"Елементът трябва да съответства на носията и да е подходящ за ${outfitSetType.ToString()}";
					return this.View(model);
				}

				await this.outfitPartService.EditByIdAsync(id, model);

				//Add images to the outfitPart
				if (elementImages.Count > 0)
				{
					// Call Add from ImageController without redirecting
					var imageController = new ImageController(imageService, webHostEnvironment);
					imageController.ControllerContext = ControllerContext;

					string entityType = EntityTypesConst.OutfitPart;

					// Invoke AddImagesOnEntityCreate Action 
					await imageController.UploadImagesAsync(id, entityType, elementImages);

				}

				this.TempData[SuccessMessage] = "Промените са запазени успешно!";
				return this.RedirectToAction("Details", "OutfitSet", new { id = model.OutfitSetId, Area = "" });
			}
			catch (Exception)
			{
				this.ModelState.AddModelError(string.Empty,
					"Възникна грешка при обработване на заявката. Моля опитайте отново или се свържете с администратор!");

				model.OutfitSets = await this.outfitSetService.GetOutfitSetsAsOptionsAsync();

				return this.View(model);
			}
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				bool outfitPartExists = await this.outfitPartService
					.ExistByIdAsync(id);
				if (!outfitPartExists)
				{
					this.TempData[ErrorMessage] = "Елемент на носия с този идентификатор не съществува!";

					return this.RedirectToAction("All", "OutfitSet", new { Area = "" });
				}

				OutfitPartForDelete viewModel =
					await this.outfitPartService.GetOutfitPartForDeleteAsync(id);

				return this.View(viewModel);
			}
			catch (Exception)
			{
				return this.GeneralError();
			}
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id, OutfitSetForDelete model)
		{
			try
			{
				bool outfitPartExists = await this.outfitPartService
					.ExistByIdAsync(id);
				if (!outfitPartExists)
				{
					this.TempData[ErrorMessage] = "Елемент на носия с този идентификатор не съществува!";

					return this.RedirectToAction("All", "OutfitSet", new { Area = "" });
				}

				await this.imageService.DeleteRelatedImagesByParentIdAsync(id, EntityTypesConst.OutfitPart, this.webHostEnvironment.WebRootPath);
				var outfitSetId = await this.outfitPartService.DeleteByIdAsyncAndReturnParentId(id);

				this.TempData[WarningMessage] = "Елементът беше изтрит успешно!";
				return this.RedirectToAction("Details", "OutfitSet", new { Id = outfitSetId, Area = "" });
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
