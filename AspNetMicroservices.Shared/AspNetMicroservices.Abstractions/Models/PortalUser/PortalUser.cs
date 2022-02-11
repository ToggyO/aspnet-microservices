using AspNetMicroservices.Abstractions.Contracts;

namespace AspNetMicroservices.Abstractions.Models.PortalUser
{
	/// <inheritdoc cref="IPortalUser"/>.
	public abstract class PortalUser<TUserDetails> : IPortalUser
		where TUserDetails : PortalUserDetails
	{
		/// <inheritdoc cref="IPortalUser.Id"/>.
		public int Id { get; set; }

		/// <inheritdoc cref="IPortalUser.FirstName"/>.
		public string FirstName { get; set; }

		/// <inheritdoc cref="IPortalUser.LastName"/>.
		public string LastName { get; set; }

		/// <inheritdoc cref="IPortalUser.Email"/>.
		public string Email { get; set; }

		/// <summary>
		/// Detailed information about user.
		/// </summary>
		public TUserDetails Details { get; set; }
	}
}