namespace NosiYa.Web.ViewModels.OutfitSet
{

    public class OutfitSetDetailsViewModel
    {
	    public int Id { get; set; }

	    public string Name { get; set; } = null!;

	    public string? Description { get; set; }

	    public string Region { get; set; } = null!;

		public decimal PricePerDay { get; set; }

	    public string RenterType { get; set; } = null!;

		public bool IsAvailable { get; set; }

		public string Size { get; set; } = null!;

/*	    public ICollection<Image> Images { get; set; }

	    public ICollection<OutfitPart> OutfitParts { get; set; }*/

	}
}
