namespace NosiYa.Web.ViewModels.OutfitPart
{
	public class OutfitPartForDelete
	{
		public string Name { get; set; } = null!;
		public string? Description { get; set; }
		public string OutfitPartType { get; set; } = null!;
		public string OwnerEmail { get; set; } = null!;

		public string RenterType { get; set; } = null!;

		public string OutfitSetName { get; set; } = null!;

		public ICollection<string> Images = new HashSet<string>();
	}
}
