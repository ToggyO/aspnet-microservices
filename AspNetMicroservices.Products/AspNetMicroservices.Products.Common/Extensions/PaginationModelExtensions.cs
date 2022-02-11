using AspNetMicroservices.Abstractions.Models.Pagination;
using AspNetMicroservices.Grpc.Protos.Common;

namespace AspNetMicroservices.Products.Common.Extensions
{
	/// <summary>
	/// Extensions for <see cref="PaginationModel{TItem}"/>.
	/// </summary>
	public static class PaginationModelExtensions
	{
		/// <summary>
		/// Converts source model into <see cref="PaginationDto"/>.
		/// </summary>
		/// <param name="model">Instance of <see cref="PaginationModel"/>.</param>
		/// <typeparam name="TSource">Type of paginated data.</typeparam>
		/// <returns>Instance of <see cref="PaginationDto"/>.</returns>
		public static PaginationDto ToPaginationDto<TSource>(
			this PaginationModel<TSource> model) => new PaginationDto
			{
				Page = model.Page,
				PageSize = model.PageSize,
				Total = model.Total,
			};
	}
}