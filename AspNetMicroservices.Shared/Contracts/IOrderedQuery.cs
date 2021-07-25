namespace AspNetMicroservices.Shared.Contracts
{
	/// <summary>
	/// Query parameters for list ordering.
	/// </summary>
	public interface IOrderedQuery
	{
		/// <summary>
		/// Field name to order by.
		/// </summary>
		public string OrderBy { get; set; }

		/// <summary>
		/// Is order by descending.
		/// </summary>
		public bool IsDesc { get; set; }
	}
}