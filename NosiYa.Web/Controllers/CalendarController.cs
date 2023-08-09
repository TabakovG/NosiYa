namespace NosiYa.Web.Controllers
{
	using System.Globalization;

	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	using Newtonsoft.Json;
	using NosiYa.Services.Data.Interfaces;
	using static Common.NotificationMessagesConstants;
	using static Common.SeedingConstants;

	public class CalendarController : BaseController
	{

		private readonly ICalendarService calendarService;

		public CalendarController(ICalendarService calendarService)
		{
			this.calendarService = calendarService;
		}

		[HttpGet]
		[Authorize(Roles = $"{AdminRoleName}, {UserRoleName}")]
		public async Task<string> PopulateCalendar(string start, string end, int id)
		{
			DateTime startDate;
			DateTime endDate;

			if (!DateTime.TryParseExact(start, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate))
			{
				this.TempData[ErrorMessage] =
					"Unexpected error occurred during calendar population request! ";
				return "";
			}
			if (!DateTime.TryParseExact(end, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate))
			{
				this.TempData[ErrorMessage] =
					"Unexpected error occurred during calendar population request! ";
				return "";
			}

			try
			{
				var reservedDates = await this.calendarService.GetReservedDatesForItemAsync(startDate, endDate, id);

				return JsonConvert.SerializeObject(reservedDates);
			}
			catch (Exception)
			{
				this.TempData[ErrorMessage] =
					"Unexpected error occurred during calendar population! ";
				return "";
			}
		}
	}
}
