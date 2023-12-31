﻿namespace NosiYa.Services.Data
{
	using Microsoft.EntityFrameworkCore;

	using NosiYa.Data;
	using NosiYa.Data.Models.Outfit;
	using Interfaces;
	using Web.ViewModels.Cart;
	using Web.ViewModels.Order;
	using static Common.ApplicationConstants;

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

		public async Task<bool> IsApprovedByIdAsync(string orderId)
		{
			return await this.context
				.OutfitRenterDates
				.Where(o => o.OrderId.ToString() == orderId)
				.AnyAsync(o => o.IsApproved);
		}

		public async Task<ICollection<OrderViewModel>> GetOrdersByUserIdAsync(string userId)
		{
			var result = await this.context
				.OutfitRenterDates
				.AsNoTracking()
				.Include(o => o.Outfit)
				.Include(o => o.Outfit.Images)
				.Where(o => o.IsActive)
				.Where(o => o.RenterId.ToString() == userId)
				.Select(o => new OrderViewModel
				{
					OrderId = o.OrderId.ToString(),
					OutfitId = o.OutfitId,
					Name = o.Outfit.Name,
					SetImage = o.Outfit.Images.Where(i => i.IsDefault).Select(i => i.Url).FirstOrDefault() ?? "",
					FromDate = o.DateRangeStart,
					ToDate = o.DateRangeEnd,
					IsApproved = o.IsApproved
				})
				.OrderBy(x => x.FromDate)
				.ThenBy(x => x.ToDate)
				.ToArrayAsync();

			return result;
		}

		public async Task<OrderDetailsViewModel> GetOrderDetailsByIdAsync(string orderId)
		{
			return await this.context
				.OutfitRenterDates
				.AsNoTracking()
				.Include(o => o.Outfit)
				.Include(o => o.Outfit.Images)
				.Where(o => o.OrderId.ToString() == orderId)
				.Select(o => new OrderDetailsViewModel
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

		public async Task<IEnumerable<OrderApprovalViewModel>> GetAllForApproval()
		{
			return await this.context
				.OutfitRenterDates
				.AsNoTracking()
				.Where(o => o.IsApproved == false && o.IsActive)
				.Select(o => new OrderApprovalViewModel
				{
					OutfitName = o.Outfit.Name,
					OutfitId = o.OrderId.ToString(),
					UserName = o.Renter.UserName,
					FromDate = o.DateRangeStart.ToString("dd-MM-yyyy"),
					ToDate = o.DateRangeEnd.ToString("dd-MM-yyyy")
				})
				.ToArrayAsync();
		}

		public async Task<bool> ExistsByIdAsync(string orderId)
		{
			return await this.context
				.OutfitRenterDates
				.Where(x => x.IsActive)
				.AnyAsync(o => o.OrderId.ToString() == orderId);
		}

		public async Task ApproveByIdAsync(string orderId)
		{
			var order = await this.context
				.OutfitRenterDates
				.FirstAsync(o => o.OrderId == Guid.Parse(orderId));

			order.IsApproved = true;

			await this.context.SaveChangesAsync();
		}

		public async Task DeleteOrderAsync(string orderId)
		{
			OutfitRenterDate order = await this.context
				.OutfitRenterDates
				.Where(o => o.OrderId == Guid.Parse(orderId))
				.Where(o => o.IsActive)
				.FirstAsync();

			order.IsActive = false;

			await this.context.SaveChangesAsync();
		}

		public async Task<bool> IsOwnedByTheUserAsync(string orderId, string userId)
		{
			return await this.context
				.OutfitRenterDates
				.Where(x => x.IsActive)
				.AnyAsync(o => o.OrderId.ToString() == orderId && o.RenterId.ToString() == userId);
		}

		public async Task<bool> IsOnTimeAsync(string orderId)
		{
			return await this.context
				.OutfitRenterDates
				.Where(o => o.IsActive)
				.AnyAsync(o => DateTime.UtcNow.AddDays(AllowedDaysBeforeRentStartOnUserDelete) <= o.DateRangeStart);
		}
	}
}
