using AspNetMicroservices.Shared.Contracts;

namespace AspNetMicroservices.Shared.Models.QueryFilter
{
	public interface IQueryFilter : IPaginatedQuery
	{
		/// <summary>
		/// Search string.
		/// </summary>
		string Search { get; set; }

	}
}