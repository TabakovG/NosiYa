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

		public async Task<bool> ValidateDatesAsync(DateTime start, DateTime end, int setId)
		{
			var isReserved = false;

			var currentDate = start.Date;
			while (currentDate <= end.Date)
			{
				isReserved = await this.context
					.OutfitRenterDates
					.AnyAsync(d => d.Date == currentDate && d.OutfitId == setId);
				if (isReserved)
				{
					break;
				}
				currentDate = currentDate.AddDays(+1);
			}
			return !isReserved;
		}
	}
}
