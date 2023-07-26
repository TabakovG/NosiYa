using NosiYa.Web.ViewModels.OutfitSet;

namespace NosiYa.Web.ViewModels.Rent
{
	public class CartFormModel
	{
		public OutfitSetForRentViewModel OutfitModel { get; set; } = null!;

		public DateTime FromDate { get; set; }

		public DateTime ToDate { get; set; }

		public ICollection<string> ReservedDates { get; set; } = new HashSet<string>();

		public int CartId { get; set; }
	}
}
