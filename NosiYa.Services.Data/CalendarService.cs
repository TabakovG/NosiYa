namespace NosiYa.Services.Data
{
	using Microsoft.EntityFrameworkCore;

	using static Common.ApplicationConstants;
	using NosiYa.Data;
	using Interfaces;
	using Models;

	public class CalendarService : ICalendarService
	{
		private readonly NosiYaDbContext context;

		public CalendarService(NosiYaDbContext _context)
		{
			this.context = _context;
		}

		public async Task<ICollection<ReservationsServiceModel>> GetReservedDatesForItemAsync(DateTime start, DateTime end, int setId)
		{
			var reservations = await this.context
				.OutfitRenterDates
				.AsNoTracking()
				.Where(x => x.IsActive)
				.Where(x => x.OutfitId == setId)
				.Where(x => (x.DateRangeStart >= start.Date && x.DateRangeStart <= end.Date)
							|| (x.DateRangeEnd >= start.Date && x.DateRangeEnd <= end.Date))
				.Select(r => new ReservationsServiceModel
				{
					title = r.IsApproved ? Reserved : WaitingForReview,
					start = r.DateRangeStart.ToString("yyyy-MM-ddTHH:mm:ss"),
					end = r.DateRangeEnd.AddDays(1).ToString("yyyy-MM-ddTHH:mm:ss"), // Add one day as the view is excluding the end date for allDay events
					backgroundColor = r.IsApproved ? "green" : "gray"
				})
				.ToArrayAsync();

			return reservations;
		}

		public async Task<ICollection<ReservationsEditServiceModel>> GetAllRentsAsync(DateTime start, DateTime end)
		{
			var reservations = await this.context
				.OutfitRenterDates
				.AsNoTracking()
				.Include(o=>o.Outfit)
				.Where(x => x.IsActive)
				.Where(x => (x.DateRangeStart >= start.Date && x.DateRangeStart <= end.Date)
				            || (x.DateRangeEnd >= start.Date && x.DateRangeEnd <= end.Date))
				.Select(r => new ReservationsEditServiceModel
				{
					id = r.OrderId.ToString(),
					title = r.Outfit.Name,
					start = r.DateRangeStart.ToString("yyyy-MM-ddTHH:mm:ss"),
					end = r.DateRangeEnd.AddDays(1).ToString("yyyy-MM-ddTHH:mm:ss") , // Add one day as the view is excluding the end date for allDay events
					backgroundColor = r.IsApproved ? "green" : "gray"
				})
				.ToArrayAsync();

			return reservations;
		}

		public async Task<bool> ValidateDatesAsync(DateTime start, DateTime end, int setId)
		{
			return await this.context
					.OutfitRenterDates
					.Where(r => r.IsActive)
					.Where(o => o.OutfitId == setId)
					.Where(x =>
						(x.DateRangeStart >= start && x.DateRangeStart <= end)
						|| (x.DateRangeEnd >= start && x.DateRangeEnd <= end))
					.AnyAsync();
		}
	}
}
