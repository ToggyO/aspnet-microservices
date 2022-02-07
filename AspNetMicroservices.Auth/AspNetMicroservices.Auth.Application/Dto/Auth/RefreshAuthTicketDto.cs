namespace AspNetMicroservices.Auth.Application.Dto.Auth
{
	/// <summary>
	/// Represents authentication ticket refreshing data transfer object.
	/// </summary>
	public class RefreshAuthTicketDto
	{
		/// <summary>
		/// Unique user identifier.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Authentication ticket identity identifier.
		/// </summary>
		public string AuthenticationTicketId { get; set; }
	}
}