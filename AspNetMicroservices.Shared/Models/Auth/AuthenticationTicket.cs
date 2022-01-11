using AspNetMicroservices.Shared.Contracts;
using AspNetMicroservices.Shared.Models.PortalUser;

namespace AspNetMicroservices.Shared.Models.Auth
{
	/// <summary>
	/// Represents authenticated user information.
	/// </summary>
	public class AuthenticationTicket<TUser> where TUser : IPortalUser
	{
		/// <summary>
		/// Current portal user information.
		/// </summary>
		public TUser User { get; set; }

		/// <summary>
		/// Auth token information.
		/// </summary>
		public TokenDto Tokens { get; set; }
	}
}