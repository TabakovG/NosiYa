namespace NosiYa.Web.ViewModels.Comment
{
	using Data.Models;

	public class CommentViewModel
	{
		public int Id { get; set; }

		public string Content { get; set; } = null!;

		public string OwnerId { get; set; } = null!;
		public string OwnerEmail { get; set; } = null!;

		public DateTime CreatedOn { get; set; }


	}
}
