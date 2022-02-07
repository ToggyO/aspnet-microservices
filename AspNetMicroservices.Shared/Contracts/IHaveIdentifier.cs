namespace AspNetMicroservices.Shared.Contracts
{
	/// <summary>
	/// Indicates that class has identity.
	/// </summary>
	public interface IHaveIdentifier
	{
		/// <summary>
		/// Item unique identity.
		/// </summary>
		int Id { get; set; }
	}
}
