namespace NosiYa.Web.ViewModels.Region
{

	public class RegionDetailsViewModel
	{
		public RegionDetailsViewModel()
		{
			this.Images = new HashSet<string>();
		}

		public int Id { get; set; }

		public string Name { get; set; } = null!;

		public string Description { get; set; } = null!;

		public ICollection<string> Images { get; set; }

	}
}
