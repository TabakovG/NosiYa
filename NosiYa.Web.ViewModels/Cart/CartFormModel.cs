using System.ComponentModel.DataAnnotations;
using NosiYa.Web.ViewModels.OutfitSet;

namespace NosiYa.Web.ViewModels.Cart
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

	}
}
