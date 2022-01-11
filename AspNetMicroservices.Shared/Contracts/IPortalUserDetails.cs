namespace AspNetMicroservices.Shared.Contracts
{
	/// <summary>
	/// Portal user detailed information.
	/// </summary>
	public interface IPortalUserDetails : IHaveIdentifier
	{
		/// <summary>
		/// Living address.
		/// </summary>
		public string Address { get; set; }

		/// <summary>
		/// Phone number.
		/// </summary>
		public string PhoneNumber { get; set; }

		/// <summary>
		/// User identifier.
		/// </summary>
		public int UserId { get; set; }
	}
}