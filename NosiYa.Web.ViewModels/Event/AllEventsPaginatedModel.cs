namespace NosiYa.Web.ViewModels.Event
{
	using System.ComponentModel.DataAnnotations;
	using static Common.ApplicationConstants;
	public class AllEventsPaginatedModel
	{
		public AllEventsPaginatedModel()
		{
			CurrentPage = DefaultFirstPage;
			EventsPerPage = DefaultResultsPerPage;
			this.Events = new HashSet<EventAllViewModel>();
		}


		public int CurrentPage { get; set; }

		[Display(Name = "Показвай по:")]
		public int EventsPerPage { get; set; }

		public int EventsCount { get; set; }

		public IEnumerable<EventAllViewModel> Events { get; set; }
	}
}
