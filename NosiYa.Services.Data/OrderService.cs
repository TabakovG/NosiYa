using Microsoft.EntityFrameworkCore;
using NosiYa.Data;
using NosiYa.Data.Models.Outfit;
using NosiYa.Services.Data.Interfaces;
using NosiYa.Web.ViewModels.Cart;
using NosiYa.Web.ViewModels.Order;

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

			var result = new OutfitRenterDate
			{
				OrderId = Guid.NewGuid(),
				OutfitId = model.OutfitId,
				RenterId = Guid.Parse(userId),
				DateRangeStart = model.FromDate,
				DateRangeEnd = model.ToDate
			};


			await this.context.AddAsync(result);
			await this.context.SaveChangesAsync();
		}

		public async Task<ICollection<OrderViewModel>> GetOrdersByUserIdAsync(string userId)
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
				.Select(o => new OrderViewModel
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

		public async Task<OrderDetailsViewModel> GetOrderDetailsByIdAsync(string orderId)
		{
			return await this.context
				.OutfitRenterDates
				.AsNoTracking()
				.Include(o=>o.Outfit)
				.Include(o=>o.Outfit.Images)
				.Where(o => o.OrderId.ToString() == orderId)
				.Select(o=> new OrderDetailsViewModel
				{
					OrderId = o.OrderId.ToString(),
					OutfitId = o.OutfitId,
					Name = o.Outfit.Name,
					SetImage = o.Outfit.Images.Where(i => i.IsDefault)
						           .Select(i => i.Url)
						           .FirstOrDefault() ??
					           "",
					FromDate = o.DateRangeStart,
					ToDate = o.DateRangeEnd,
					IsApproved = o.IsApproved,
					Username = o.Renter.UserName,
					Email = o.Renter.Email,
					Phone = o.Renter.PhoneNumber

				})
				.FirstAsync();
		}

		public async Task<bool> ExistsByIdAsync(string orderId)
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
