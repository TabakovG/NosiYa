namespace NosiYa.Services.Data.Models
{
using Web.ViewModels.Event;

	public class AllEventsPagedServiceModel
	{
		public AllEventsPagedServiceModel()
		{
			this.Events = new HashSet<EventAllViewModel>();
		}
		public int EventsCount { get; set; }
		public IEnumerable<EventAllViewModel> Events { get; set; }
	}
}
	