namespace NosiYa.Services.Data
{
	using Microsoft.EntityFrameworkCore;

	using NosiYa.Data;
	using NosiYa.Data.Models.Outfit;
	using Web.ViewModels.Cart;
	using static Common.ApplicationConstants;
	using Interfaces;
	using Models;

	public class CartService : ICartService
	{
		private readonly NosiYaDbContext context;

		public CartService(NosiYaDbContext _context)
		{
			this.context = _context;
		}

		public async Task<bool> CartItemExistsById(int id)
		{
			return await this.context
				.OutfitsForCarts
				.Where(x => x.IsActive)
				.AnyAsync(o => o.Id == id);
		}

		public async Task DeleteItemFromUserCartAsync(int id)
		{
			var item = await this.context
				.OutfitsForCarts
				.Where(x=>x.IsActive)
				.FirstAsync(x=>x.Id == id);

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

		public async Task<ICollection<ReservationsServiceModel>> GetReservedDates(DateTime start, DateTime end) //TODO can be moved in separate controller ??
		{
			var reservations = await this.context
				.OutfitRenterDates
				.Where(x => x.Date >= start.Date && x.Date <= end.Date)
				.Where(x => x.IsActive)
				.Select(r => new ReservationsServiceModel
				{
					title = r.IsApproved ? Reserved : WaitingForReview,
					date = r.Date.ToString("yyyy-MM-ddTHH:mm:ss"),
					backgroundColor = r.IsApproved ? "green" : "gray"
				})
				.ToArrayAsync();

			return reservations;
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
					Date = currentDate.Date
				});
				currentDate = currentDate.AddDays(+1);
			}

			await this.context.AddRangeAsync(result);
			await this.context.SaveChangesAsync();
		}

		public async Task<ICollection<CartPreOrderViewModel>> GetAllItemsFromUserCartAsync(string userId)
		{
			var result = await this.context
				.OutfitsForCarts
				.AsNoTracking()
				.Where(o=>o.Cart.OwnerId.ToString() == userId)
				.Where(x => x.IsActive)
				.Select(o=>new CartPreOrderViewModel
				{
					Id = o.Id,
					OutfitId = o.OutfitId,
					Name = o.OutfitSet.Name,
					setImage = o.OutfitSet.Images.Where(i=>i.IsDefault).Select(i=>i.Url).First(),
					FromDate = o.FromDate,
					ToDate = o.ToDate
				})
				.ToArrayAsync();

			return result;
		}

		public async Task<int> GetCartIdByUserIdAsync(string userId)
		{
			return await this.context
				.Carts
				.Where(c => c.Owner.Id.ToString() == userId)
				.Select(c => c.Id)
				.FirstAsync();
		}

		public async Task CreateCartItemAsync(CartPreOrderFormModel model)
		{
			var rentRequest = new OutfitForCart
			{
				OutfitId = model.OutfitModel.Id,
				FromDate = model.FromDate,
				ToDate = model.ToDate,
				CartId = model.CartId
			};

			await this.context.OutfitsForCarts.AddAsync(rentRequest);
			await this.context.SaveChangesAsync();
		}
	}
}
