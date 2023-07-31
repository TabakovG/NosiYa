namespace NosiYa.Web.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using Newtonsoft.Json;
	using NosiYa.Services.Data.Interfaces;
	using System.Globalization;
	using static Common.NotificationMessagesConstants;

	public class CalendarController : Controller
	{

		private readonly ICalendarService calendarService;

		public CalendarController(ICalendarService calendarService)
		{
			this.calendarService = calendarService;
		}

		[HttpGet]
		public async Task<string> PopulateCalendar(string start, string end)
		{
			try
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

				var reservedDates = await this.calendarService.GetReservedDates(startDate, endDate);

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
