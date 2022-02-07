namespace AspNetMicroservices.Shared.Contracts
{
	/// <summary>
	/// Represents portal user.
	/// </summary>
	public interface IPortalUser : IHaveIdentifier
	{
		/// <summary>
		/// First name.
		/// </summary>
		public string FirstName { get; set; }

		/// <summary>
		/// Last name.
		/// </summary>
		public string LastName { get; set; }

		/// <summary>
		/// Email.
		/// </summary>
		public string Email { get; set; }
	}
}