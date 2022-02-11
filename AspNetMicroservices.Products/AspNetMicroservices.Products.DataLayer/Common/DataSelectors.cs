using System.Linq;
using System.Threading.Tasks;

using AspNetMicroservices.Abstractions.Contracts;

using LinqToDB;

namespace AspNetMicroservices.Products.DataLayer.Common
{
	/// <summary>
	/// Query selectors.
	/// </summary>
	public static class DataSelectors
	{
		/// <summary>
		/// Select items with specified identifier.
		/// </summary>
		/// <typeparam name="TItem">Item type.</typeparam>
		/// <param name="items">Source items.</param>
		/// <param name="id">Identifier for filtering.</param>
		/// <returns>Items query.</returns>
		public static IQueryable<TItem> WithId<TItem>(
			this IQueryable<TItem> items,
			int id)
			where TItem : IHaveIdentifier
			=> items.Where(item => item.Id == id);

		/// <summary>
		/// Select item with specified identifier.
		/// </summary>
		/// <typeparam name="TItem">Item type.</typeparam>
		/// <param name="items">Source items.</param>
		/// <param name="id">Identifier for filtering.</param>
		/// <returns>Item by ID.</returns>
		public static Task<TItem> GetById<TItem>(
			this IQueryable<TItem> items,
			int id) where TItem : IHaveIdentifier
			=> items.WithId(id).SingleOrDefaultAsync();

		/// <summary>
		/// Select item with specified identifier.
		/// </summary>
		/// <typeparam name="TItem">Item type.</typeparam>
		/// <param name="items">Source items.</param>
		/// <param name="id">Identifier for filtering.</param>
		/// <returns>Item by ID.</returns>
		public static async Task<bool> EntityExists<TItem>(this IQueryable<TItem> items, int id)
			where TItem : IHaveIdentifier
			=> await items.WithId(id).AnyAsync();

	}
}