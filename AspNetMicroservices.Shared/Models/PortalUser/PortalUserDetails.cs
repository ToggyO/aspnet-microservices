using AspNetMicroservices.Shared.Contracts;

namespace AspNetMicroservices.Shared.Models.PortalUser
{
	/// <summary>
	/// Represents portal user detailed information.
	/// </summary>
	public abstract class PortalUserDetails : IPortalUserDetails
	{
		/// <inheritdoc cref="IPortalUserDetails.Id"/>.
		public int Id { get; set; }

		/// <inheritdoc cref="IPortalUserDetails.Address"/>.
		public string Address { get; set; }

		/// <inheritdoc cref="IPortalUserDetails.PhoneNumber"/>.
		public string PhoneNumber { get; set; }

		/// <inheritdoc cref="IPortalUserDetails.UserId"/>.
		public int UserId { get; set; }
	}
}