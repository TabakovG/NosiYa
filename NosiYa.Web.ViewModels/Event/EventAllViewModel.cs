namespace NosiYa.Web.ViewModels.Event
{

	public class EventAllViewModel
	{
		public int Id { get; set; }

		public string Name { get; set; } = null!;
		public string OwnerId { get; set; } = null!;

		public DateTime EventStartDate { get; set; }
		public DateTime EventEndDate { get; set; }

		public string ImageUrl { get; set; } = null!;
	}
}
