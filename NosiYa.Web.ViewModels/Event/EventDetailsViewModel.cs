using NosiYa.Web.ViewModels.Comment;

namespace NosiYa.Web.ViewModels.Event
{
	using Data.Models;

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

		public Guid OwnerId { get; set; }
		public ApplicationUser Owner { get; set; } = null!;

		public DateTime EventStartDate { get; set; }

		public DateTime EventEndDate { get; set; }

		public ICollection<CommentViewModel> Comments { get; set; }

		public ICollection<string> Images { get; set; }
	}
}
