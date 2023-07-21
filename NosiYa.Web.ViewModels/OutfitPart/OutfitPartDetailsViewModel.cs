namespace NosiYa.Web.ViewModels.OutfitPart
{
	public class OutfitPartDetailsViewModel
	{
		public OutfitPartDetailsViewModel()
		{
			this.Images = new HashSet<string>();
		}

		public int Id { get; set; }

		public string Name { get; set; } = null!;

		public string? Description { get; set; }

		public string Color { get; set; } = null!;

		public string RenterType { get; set; } = null!;

		public string OutfitPartType { get; set; } = null!;

		public string OutfitSet { get; set; } = null!;

		public string OwnerEmail { get; set; } = null!;

		public string Size { get; set; } = null!;

		public ICollection<string> Images { get; set; }

	}
}
