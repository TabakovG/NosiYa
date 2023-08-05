using NosiYa.Web.ViewModels.OutfitSet;

namespace NosiYa.Services.Data
{
	using Microsoft.EntityFrameworkCore;

	using NosiYa.Data;
	using NosiYa.Data.Models.Outfit;
	using Web.ViewModels.Cart;
	using Interfaces;

	public class CartService : ICartService
	{
		private readonly NosiYaDbContext context;

		public CartService(NosiYaDbContext _context)
		{
			this.context = _context;
		}

		public async Task<bool> CartItemExistsById(int id)
		{
			return await this.context
				.OutfitsForCarts
				.Where(x => x.IsActive)
				.AnyAsync(o => o.Id == id);
		}

		public async Task<CartItemFormModel> GetForEditByIdAsync(int id)
		{
			var item = await this.context
				.OutfitsForCarts
				.Where(x => x.IsActive)
				.FirstAsync(x => x.Id == id);

			var formModel = new CartItemFormModel
			{
				CartId = item.CartId,
				OutfitSetId = item.OutfitId,
				FromDate = item.FromDate,
				ToDate = item.ToDate,

			};

			return formModel;
		}

		public async Task EditByIdAsync(int id, CartItemFormModel model)
		{
			var item = await this.context
				.OutfitsForCarts
				.Where(x => x.IsActive)
				.FirstAsync(x => x.Id == id);

			item.FromDate = model.FromDate;
			item.ToDate = model.ToDate;

			await this.context.SaveChangesAsync();
		}

		public async Task DeleteItemFromUserCartAsync(int id)
		{
			var item = await this.context
				.OutfitsForCarts
				.Where(x => x.IsActive)
				.FirstAsync(x => x.Id == id);

			item.IsActive = false;

			await this.context.SaveChangesAsync();
		}


		public async Task<ICollection<CartItemsViewModel>> GetAllItemsFromUserCartAsync(string userId)
		{
			var result = await this.context
				.OutfitsForCarts
				.AsNoTracking()
				.Where(o => o.Cart.OwnerId.ToString() == userId)
				.Where(x => x.IsActive)
				.Select(o => new CartItemsViewModel
				{
					Id = o.Id,
					OutfitId = o.OutfitId,
					Name = o.OutfitSet.Name,
					SetImage = o.OutfitSet.Images.Where(i => i.IsDefault).Select(i => i.Url).First(),
					FromDate = o.FromDate,
					ToDate = o.ToDate
				})
				.ToArrayAsync();

			return result;
		}

		

		public async Task<int> GetCartIdByUserIdAsync(string userId)
		{
			return await this.context
				.Carts
				.Where(c => c.Owner.Id.ToString() == userId)
				.Select(c => c.Id)
				.FirstAsync();
		}

		public async Task CreateCartItemAsync(CartItemFormModel model)
		{
			var rentRequest = new OutfitForCart
			{
				OutfitId = model.OutfitSetId,
				FromDate = model.FromDate,
				ToDate = model.ToDate,
				CartId = model.CartId
			};

			await this.context.OutfitsForCarts.AddAsync(rentRequest);
			await this.context.SaveChangesAsync();
		}
	}
}
