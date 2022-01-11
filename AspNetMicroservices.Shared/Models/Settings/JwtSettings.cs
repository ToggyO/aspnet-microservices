namespace AspNetMicroservices.Shared.Models.Settings
{
	/// <summary>
	/// JWT token configuration settings.
	/// </summary>
	public class JwtSettings
	{
		/// <summary>
		/// Token secret key.
		/// </summary>
		public string PrivateKey { set; get; }

		/// <summary>
		/// Access token expiration time in minutes.
		/// </summary>
		public int AccessTokenExpiresInMinutes { set; get; }

		/// <summary>
		/// Refresh token expiration time in minutes.
		/// </summary>
		public int RefreshTokenExpiresInMinutes { set; get; }

		/// <summary>
		/// Token issuer.
		/// </summary>
		public string Issuer { set; get; }

		/// <summary>
		/// Token audience.
		/// </summary>
		public string Audience { set; get; }
	}
}