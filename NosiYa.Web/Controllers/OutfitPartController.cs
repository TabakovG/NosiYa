namespace NosiYa.Web.Controllers
{
	using Microsoft.AspNetCore.Mvc;

	using NosiYa.Services.Data.Interfaces;

	using ViewModels.OutfitSet;
	using ViewModels.OutfitPart;

	public class OutfitPartController : Controller
	{
		private readonly IOutfitPartService outfitPartService;
		private readonly IOutfitSetService outfitSetService;
		private readonly IUserService userService;

		public OutfitPartController(IOutfitPartService outfitPartService, IOutfitSetService outfitSetService, IUserService userService)
		{
			this.outfitPartService = outfitPartService;
			this.outfitSetService = outfitSetService;
			this.userService = userService;
		}

		[HttpGet]
		public async Task<IActionResult> Add(int id)
		{
			try
			{
				var outfitPart = new OutfitPartFormModel()
				{
					OutfitSets = await this.outfitSetService.GetAllOutfitSetsForOptionsAsync(),
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
		public async Task<IActionResult> Add(OutfitPartFormModel model)
		{
			try
			{
				//TODO To check if the user is admin

				if (!this.ModelState.IsValid)
				{
					model.OutfitSets = await this.outfitSetService.GetAllOutfitSetsForOptionsAsync();
					
					return this.View(model);
				}

				var isAuthenticated = this.User?.Identity?.IsAuthenticated ?? false;

				if (!isAuthenticated)
				{
					model.OutfitSets = await this.outfitSetService.GetAllOutfitSetsForOptionsAsync();
					return this.View(model);
				}

				var ownerExist = await this.userService.UserExistByEmail(model.OwnerEmail);
				if (!ownerExist)
				{
					model.OutfitSets = await this.outfitSetService.GetAllOutfitSetsForOptionsAsync();
					this.TempData["ErrorMessage"] = "Посочения собственик на елемента няма акаунт в системата!";
					return this.View(model);
				}

				model.OwnerId = await this.userService.GetUserIdFromEmailAsync(model.OwnerEmail);

				try
				{
					int outfitPartId =
						await this.outfitPartService.CreateAndReturnIdAsync(model);

					return this.RedirectToAction("Details", "OutfitSet", new { Id = model.OutfitSetId });
				}
				catch (Exception)
				{
					model.OutfitSets = await this.outfitSetService.GetAllOutfitSetsForOptionsAsync();
					return this.View(model);
				}
			}
			catch (Exception)
			{
				return this.GeneralError();

			}
		}

		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			var outfitPartExists = await this.outfitPartService.ExistByIdAsync(id);

			if (!outfitPartExists)
			{
				this.TempData["ErrorMessage"] = "Елемент на носия с този идентификатор не съществува!";

				return this.RedirectToAction("All", "OutfitSet");
			}

			try
			{
				OutfitPartDetailsViewModel viewModel = await this.outfitPartService
					.GetDetailsByIdAsync(id);


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
			var outfitPartExists = await this.outfitPartService.ExistByIdAsync(id);

			if (!outfitPartExists)
			{
				this.TempData["ErrorMessage"] = "Елемент на носия с този идентификатор не съществува!";

				return this.RedirectToAction("All", "OutfitSet");
			}

			try
			{
				OutfitPartFormModel formModel = await this.outfitPartService
					.GetForEditByIdAsync(id);
				formModel.OutfitSets = await this.outfitSetService.GetAllOutfitSetsForOptionsAsync();

				return View(formModel);
			}
			catch (Exception)
			{
				return this.GeneralError();
			}
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, OutfitPartFormModel model)
		{
			if (!this.ModelState.IsValid)
			{
				model.OutfitSets = await this.outfitSetService.GetAllOutfitSetsForOptionsAsync();
				return this.View(model);
			}

			var outfitPartExists = await this.outfitPartService.ExistByIdAsync(id);

			if (!outfitPartExists)
			{
				this.TempData["ErrorMessage"] = "Елемент на носия с този идентификатор не съществува!";

				return this.RedirectToAction("All", "OutfitSet");
			}
			var isAuthenticated = this.User?.Identity?.IsAuthenticated ?? false;

			if (!isAuthenticated)
			{
				model.OutfitSets = await this.outfitSetService.GetAllOutfitSetsForOptionsAsync();
				return this.View(model);
			}

			var ownerExist = await this.userService.UserExistByEmail(model.OwnerEmail);
			if (!ownerExist)
			{
				model.OutfitSets = await this.outfitSetService.GetAllOutfitSetsForOptionsAsync();
				this.TempData["ErrorMessage"] = "Посочения собственик на елемента няма акаунт в системата!";
				return this.View(model);
			}

			model.OwnerId = await this.userService.GetUserIdFromEmailAsync(model.OwnerEmail);

			var outfitSetType = await this.outfitSetService.GetRenterTypeByIdAsync(model.OutfitSetId);
			if (outfitSetType != model.RenterType)
			{
				model.OutfitSets = await this.outfitSetService.GetAllOutfitSetsForOptionsAsync();
				this.TempData["ErrorMessage"] = $"Елементът трябва да съответсва на носията и да е подходящ за ${outfitSetType.ToString()}";
				return this.View(model);
			}

			try
			{
				await this.outfitPartService.EditByIdAsync(id, model);
				this.TempData["SuccessMessage"] = "Промените са запазени успешно!";
				return this.RedirectToAction("Details", "OutfitSet", new { model.OutfitSetId });
			}
			catch (Exception)
			{
				this.ModelState.AddModelError(string.Empty,
					"Възникна грешка при обработване на заявката. Моля опитайте отново или се свържете с администратор!");
				
				model.OutfitSets = await this.outfitSetService.GetAllOutfitSetsForOptionsAsync();

				return this.View(model);
			}
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			bool outfitPartExists = await this.outfitPartService
				.ExistByIdAsync(id);
			if (!outfitPartExists)
			{
				this.TempData["ErrorMessage"] = "Елемент на носия с този идентификатор не съществува!";

				return this.RedirectToAction("All", "OutfitSet");
			}

			try
			{
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
			bool outfitPartExists = await this.outfitPartService
				.ExistByIdAsync(id);
			if (!outfitPartExists)
			{
				this.TempData["ErrorMessage"] = "Елемент на носия с този идентификатор не съществува!";

				return this.RedirectToAction("All", "OutfitSet");
			}

			try
			{
				var outfitSetId = await this.outfitPartService.DeleteByIdAsyncAndReturnParentId(id);

				this.TempData["WarningMessage"] = "Елементът беше изтрит успешно!";
				return this.RedirectToAction("Details", "OutfitSet", new { Id = outfitSetId });
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
