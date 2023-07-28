namespace NosiYa.Services.Data.Interfaces
{
	using NosiYa.Services.Data.Models;

	public  interface ICalendarService
	{
		Task<ICollection<ReservationsServiceModel>> GetReservedDates(DateTime start, DateTime end);

		Task<bool> ValidateDatesAsync(DateTime start, DateTime end, int setId);


	}
}
