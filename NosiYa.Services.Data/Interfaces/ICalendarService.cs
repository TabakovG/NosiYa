namespace NosiYa.Services.Data.Interfaces
{
	using NosiYa.Services.Data.Models;

	public  interface ICalendarService
	{
		Task<bool> ValidateDatesIfItemIsReservedAsync(DateTime start, DateTime end, int setId);

		Task<ICollection<ReservationsServiceModel>> GetReservedDatesForItemAsync(DateTime start, DateTime end, int outfitSetId);

		Task<ICollection<ReservationsEditServiceModel>> GetAllRentsAsync(DateTime start, DateTime end);
	}
}
