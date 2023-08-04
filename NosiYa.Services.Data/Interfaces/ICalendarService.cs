namespace NosiYa.Services.Data.Interfaces
{
	using NosiYa.Services.Data.Models;

	public  interface ICalendarService
	{
		Task<ICollection<ReservationsServiceModel>> GetReservedDatesForItemAsync(DateTime start, DateTime end, int outfitSetId);

		Task<bool> ValidateDatesAsync(DateTime start, DateTime end, int setId);


	}
}
