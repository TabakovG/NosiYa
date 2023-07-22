namespace NosiYa.Web.ViewModels.Comment
{
	using Data.Models;

	public class CommentViewModel
	{
		public int Id { get; set; }

		public string Content { get; set; } = null!;

		public Guid OwnerId { get; set; }
		public ApplicationUser Owner { get; set; } = null!;

	}
}
