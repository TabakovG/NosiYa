namespace NosiYa.Web.ViewModels.OutfitSet
{
    public class OutfitSetForDelete
    {
	    public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

		public ICollection<string> Images = new HashSet<string>();

    }
}
