namespace NosiYa.Web.ViewModels.Event
{
	using Comment;

	public class EventDetailsViewModel
	{
		public EventDetailsViewModel()
		{
			this.Comments = new HashSet<CommentViewModel>();
			this.Images = new HashSet<string>();
		}

		public int Id { get; set; }

		public string Name { get; set; } = null!;

		public string Description { get; set; } = null!;

		public string Location { get; set; } = null!;

		public string OwnerId { get; set; } = null!;
		public string Owner { get; set; } = null!;

		public DateTime EventStartDate { get; set; }

		public DateTime EventEndDate { get; set; }

		public IEnumerable<CommentViewModel> Comments { get; set; }

		public ICollection<string> Images { get; set; }

		public CommentFormModel CommentForm { get; set; } = null!;
	}
}
