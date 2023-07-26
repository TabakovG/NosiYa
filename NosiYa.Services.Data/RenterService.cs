using Microsoft.EntityFrameworkCore;
using NosiYa.Data;
using NosiYa.Data.Models.Outfit;
using NosiYa.Web.ViewModels.Rent;

namespace NosiYa.Services.Data
{
	using Interfaces;

	public class RenterService : IRenterService
	{
		private readonly NosiYaDbContext context;

		public RenterService(NosiYaDbContext _context)
		{
			this.context = _context;
		}

		public async Task<ICollection<string>> GetReservedDates()
		{
			return new HashSet<string>();
		}

		public async Task<int> GetCartIdByUserIdAsync(string userId)
		{
			return await this.context
				.Carts
				.Where(c => c.Owner.Id.ToString() == userId)
				.Select(c => c.Id)
				.FirstAsync();
		}

		public async Task CreateRentRequestAsync(CartFormModel model)
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
