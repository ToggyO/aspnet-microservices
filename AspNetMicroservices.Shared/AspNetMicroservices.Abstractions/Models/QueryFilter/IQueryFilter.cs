using AspNetMicroservices.Abstractions.Contracts;

namespace AspNetMicroservices.Abstractions.Models.QueryFilter
{
	/// <summary>
	/// Represents common filter for data querying.
	/// </summary>
	public interface IQueryFilter : IPaginatedQuery, IOrderedQuery
	{
		/// <summary>
		/// Search string.
		/// </summary>
		string Search { get; set; }

	}
}