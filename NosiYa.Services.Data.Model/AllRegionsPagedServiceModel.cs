namespace NosiYa.Services.Data.Models
{
	using Web.ViewModels.Region;
	
	public class AllRegionsPagedServiceModel
	{
		public AllRegionsPagedServiceModel()
		{
			this.Regions = new HashSet<RegionAllViewModel>();
		}
		public int RegionsCount { get; set; }
		public IEnumerable<RegionAllViewModel> Regions { get; set; }
	}
}
