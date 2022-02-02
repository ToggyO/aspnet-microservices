namespace AspNetMicroservices.Auth.Application.Dto.Auth
{
	/// <summary>
	/// Data transfer object contains refresh token.
	/// </summary>
	public class RefreshTokenDto
	{
		/// <summary>
		/// Refresh token.
		/// </summary>
		public string RefreshToken { get; set; }
	}
}