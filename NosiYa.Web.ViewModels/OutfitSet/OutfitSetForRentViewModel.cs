namespace NosiYa.Web.ViewModels.OutfitSet
{
	public class OutfitSetForRentViewModel
	{
		public string Name { get; set; } = null!;

		public string Description { get; set; } = null!;

		public string Size { get; set; } = null!;

		public string RenterType { get; set; } = null!;

		public ICollection<string> Images = new HashSet<string>();
	}
}
