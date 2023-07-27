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

		public async Task<ICollection<ReservationsServiceModel>> GetReservedDates(DateTime start, DateTime end) //TODO can be moved in separate controller ??
		{
			var reservations = await this.context
				.OutfitRenterDates
				.Where(x => x.Date >= start.Date && x.Date <= end.Date)
				.Select(r => new ReservationsServiceModel
				{
					title = r.IsActive ? Reserved : WaitingForReview,
					date = r.Date.ToString("yyyy-MM-ddTHH:mm:ss"),
					backgroundColor = r.IsActive ? "green" : "gray"
				})
				.ToArrayAsync();

			return reservations;
		}

		public async Task<int> GetCartIdByUserIdAsync(string userId)
		{
			return await this.context
				.Carts
				.Where(c => c.Owner.Id.ToString() == userId)
				.Select(c => c.Id)
				.FirstAsync();
		}

		public async Task CreateCartItemAsync(CartFormModel model)
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
