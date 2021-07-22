namespace AspNetMicroservices.Shared.Contracts
{
	/// <summary>
	/// Query parameters for specified page.
	/// </summary>
	public interface IPaginatedQuery
	{
		/// <summary>
		/// Page number to get.
		/// </summary>
		int Page { get; }

		/// <summary>
		/// Items on each page.
		/// </summary>
		int PageSize { get; }
	}
}