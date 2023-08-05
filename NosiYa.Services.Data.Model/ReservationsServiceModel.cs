﻿namespace NosiYa.Services.Data.Models
{
	public class ReservationsServiceModel
	{
		public string title { get; set; } = null!;

		public string start { get; set; } = null!;
		public string end { get; set; } = null!;

		public string backgroundColor { get; set; } = null!;
		public bool background { get; set; } = true;

		public bool allDay { get; set; } = true;

	}
}
