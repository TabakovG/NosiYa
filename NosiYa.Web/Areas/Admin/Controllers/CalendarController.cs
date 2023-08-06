namespace NosiYa.Web.Areas.Admin.Controllers
{
	using System.Globalization;

	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using Newtonsoft.Json;

	using NosiYa.Services.Data.Interfaces;
	using static Common.NotificationMessagesConstants;
	using static Common.SeedingConstants;

	public class CalendarController : BaseAdminController
	{
		private readonly ICalendarService calendarService;

		public CalendarController(ICalendarService calendarService)
		{
			this.calendarService = calendarService;
		}

		[HttpGet]
		[Authorize(Roles = $"{AdminRoleName}, {UserRoleName}")]
		public async Task<string> PopulateCalendarAll(string start, string end)
		{
			try
			{
				if (!DateTime.TryParseExact(start, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture, DateTimeStyles.None, out var startDate))
				{
					this.TempData[ErrorMessage] =
						"Unexpected error occurred during calendar population request! ";
					return "";
				}
				if (!DateTime.TryParseExact(end, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture, DateTimeStyles.None, out var endDate))
				{
					this.TempData[ErrorMessage] =
						"Unexpected error occurred during calendar population request! ";
					return "";
				}

				var reservations = await this.calendarService.GetAllRentsAsync(startDate, endDate);

				return JsonConvert.SerializeObject(reservations);
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
