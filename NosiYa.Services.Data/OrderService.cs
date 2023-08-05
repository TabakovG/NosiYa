using Microsoft.EntityFrameworkCore;
using NosiYa.Data;
using NosiYa.Data.Models.Outfit;
using NosiYa.Services.Data.Interfaces;
using NosiYa.Web.ViewModels.Cart;

namespace NosiYa.Services.Data
{
	public class OrderService : IOrderService
	{

		private readonly NosiYaDbContext context;

		public OrderService(NosiYaDbContext _context)
		{
			this.context = _context;
		}

		public async Task CreateOrderAsync(CartCompleteOrderFormModel model, string userId)
		{
			var start = model.FromDate.Date;
			var end = model.ToDate.Date;

			var result = new HashSet<OutfitRenterDate>();

			var currentDate = start;
			var orderId = Guid.NewGuid();
			while (currentDate <= end)
			{
				result.Add(new OutfitRenterDate
				{
					OrderId = orderId,
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

		public async Task<ICollection<ReservedItemsViewModel>> GetOrdersByUserIdAsync(string userId)
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
					OrderId = o.OrderId.ToString(),
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

		public async Task<bool> OrderExistsById(string orderId)
		{
			return await this.context
				.OutfitRenterDates
				.Where(x => x.IsActive)
				.AnyAsync(o => o.OrderId == Guid.Parse(orderId));
		}

		public async Task DeleteOrderAsync(string orderId)
		{
			var reservation = await this.context
				.OutfitRenterDates
				.Where(o => o.OrderId == Guid.Parse(orderId))
				.Where(o => o.IsActive)
				.ToListAsync();

			foreach (var order in reservation)
			{
				order.IsActive = false;
			}

			await this.context.SaveChangesAsync();
		}


	}
}
