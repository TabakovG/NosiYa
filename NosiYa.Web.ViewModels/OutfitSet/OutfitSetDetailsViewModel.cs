namespace NosiYa.Web.ViewModels.OutfitSet
{
    using OutfitPart;

    public class OutfitSetDetailsViewModel
    {
        public OutfitSetDetailsViewModel()
        {
            this.Images = new HashSet<string>();
            this.OutfitParts = new HashSet<OutfitPartViewModel>();
        }
	    public int Id { get; set; }

	    public string Name { get; set; } = null!;

	    public string? Description { get; set; }

	    public string Region { get; set; } = null!;

		public decimal PricePerDay { get; set; }

	    public string RenterType { get; set; } = null!;

		public bool IsAvailable { get; set; }

		public string Size { get; set; } = null!;

	    public ICollection<string> Images { get; set; }

	    public ICollection<OutfitPartViewModel> OutfitParts { get; set; }

	}
}
