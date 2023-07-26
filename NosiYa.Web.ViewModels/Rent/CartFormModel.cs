using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using NosiYa.Web.ViewModels.OutfitSet;

namespace NosiYa.Web.ViewModels.Rent
{
	public class CartFormModel
	{

		[Required]
		public int CartId { get; set; }

		public OutfitSetForRentViewModel OutfitModel { get; set; } = null!;

		[Required]
		public DateTime FromDate { get; set; }

		[Required]
		public DateTime ToDate { get; set; }

		public ICollection<string> ReservedDates { get; set; } = new HashSet<string>();
	}
}
