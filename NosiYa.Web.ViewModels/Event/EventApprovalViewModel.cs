namespace NosiYa.Web.ViewModels.Event
{
	public class EventApprovalViewModel
	{
		public string EventName { get; set; } = null!;
		public int EventId { get; set; }
		public string EventStart { get; set; } = null!;
		public string EventEnd { get; set; } = null!;
		public string UserName { get; set; } = null!;

	}
}
