using NosiYa.Web.ViewModels.OutfitSet;

namespace NosiYa.Services.Data
{
	using Microsoft.EntityFrameworkCore;

	using NosiYa.Data;
	using NosiYa.Data.Models.Outfit;
	using Web.ViewModels.Cart;
	using Interfaces;

	public class CartService : ICartService
	{
		private readonly NosiYaDbContext context;
		private readonly IOutfitSetService outfitSetService;

		public CartService(NosiYaDbContext _context, IOutfitSetService outfitSetService)
		{
			this.context = _context;
			this.outfitSetService = outfitSetService;
		}

		public async Task<bool> CartItemExistsById(int id)
		{
			return await this.context
				.OutfitsForCarts
				.Where(x => x.IsActive)
				.AnyAsync(o => o.Id == id);
		}

		public async Task<CartItemFormModel> GetForEditByIdAsync(int id)
		{
			var item = await this.context
				.OutfitsForCarts
				.Where(x => x.IsActive)
				.FirstAsync(x => x.Id == id);

			var formModel = new CartItemFormModel
			{
				CartId = item.CartId,
				OutfitSetId = item.OutfitId,
				FromDate = item.FromDate,
				ToDate = item.ToDate,

			};

			return formModel;
		}

		public async Task EditByIdAsync(int id, CartItemFormModel model)
		{
			var item = await this.context
				.OutfitsForCarts
				.Where(x => x.IsActive)
				.FirstAsync(x => x.Id == id);

			item.FromDate = model.FromDate;
			item.ToDate = model.ToDate;

			await this.context.SaveChangesAsync();
		}

		public async Task DeleteItemFromUserCartAsync(int id)
		{
			var item = await this.context
				.OutfitsForCarts
				.Where(x => x.IsActive)
				.FirstAsync(x => x.Id == id);

			item.IsActive = false;

			await this.context.SaveChangesAsync();
		}

		public async Task DeleteCompletedOrderAsync(int id, DateTime date)
		{
			var order = await this.context
				.OutfitRenterDates
				.Where(o => o.OutfitId == id && o.Date == date)
				.Where(o => o.IsActive)
				.FirstAsync();

			order.IsActive = false;
			await this.context.SaveChangesAsync();
		}

		public async Task CartOrderCompleteAsync(CartCompleteOrderFormModel model, string userId)
		{
			var start = model.FromDate.Date;
			var end = model.ToDate.Date;

			var result = new HashSet<OutfitRenterDate>();

			var currentDate = start;
			while (currentDate <= end)
			{
				result.Add(new OutfitRenterDate
				{
					OutfitId = model.OutfitId,
					RenterId = Guid.Parse(userId),
					Date = currentDate.Date,
					DateRangeStart = start,
					DateRangeEnd = end
				});
				currentDate = currentDate.AddDays(+1);
			}

			await this.context.AddRangeAsync(result);
			await this.context.SaveChangesAsync();
		}

		public async Task<ICollection<CartItemsViewModel>> GetAllItemsFromUserCartAsync(string userId)
		{
			var result = await this.context
				.OutfitsForCarts
				.AsNoTracking()
				.Where(o => o.Cart.OwnerId.ToString() == userId)
				.Where(x => x.IsActive)
				.Select(o => new CartItemsViewModel
				{
					Id = o.Id,
					OutfitId = o.OutfitId,
					Name = o.OutfitSet.Name,
					SetImage = o.OutfitSet.Images.Where(i => i.IsDefault).Select(i => i.Url).First(),
					FromDate = o.FromDate,
					ToDate = o.ToDate
				})
				.ToArrayAsync();

			return result;
		}

		public async Task<ICollection<ReservedItemsViewModel>> GetReservedItemsByUserIdAsync(string userId)
		{
			var resultDistinct = await this.context
				.OutfitRenterDates
				.AsNoTracking()
				.Include(o => o.Outfit)
				.Include(o => o.Outfit.Images)
				.Where(o => o.IsActive)
				.Where(o => o.RenterId.ToString() == userId)
				.GroupBy(o => new { o.OutfitId, o.DateRangeStart, o.DateRangeEnd })
				.Select(o => o.First())
				.ToArrayAsync();

			var modelsResult = resultDistinct
			.Select(o => new ReservedItemsViewModel
			{
				OutfitId = o.OutfitId,
				Name = o.Outfit.Name,
				SetImage = o.Outfit.Images.Where(i => i.IsDefault).Select(i => i.Url).First(),
				FromDate = o.DateRangeStart,
				ToDate = o.DateRangeEnd,
				IsApproved = o.IsApproved
			})
			.ToArray();

			return modelsResult;
		}

		public async Task<int> GetCartIdByUserIdAsync(string userId)
		{
			return await this.context
				.Carts
				.Where(c => c.Owner.Id.ToString() == userId)
				.Select(c => c.Id)
				.FirstAsync();
		}

		public async Task CreateCartItemAsync(CartItemFormModel model)
		{
			var rentRequest = new OutfitForCart
			{
				OutfitId = model.OutfitSetId,
				FromDate = model.FromDate,
				ToDate = model.ToDate,
				CartId = model.CartId
			};

			await this.context.OutfitsForCarts.AddAsync(rentRequest);
			await this.context.SaveChangesAsync();
		}
	}
}
