using System;

namespace AspNetMicroservices.Abstractions.Models.Auth
{
	/// <summary>
	/// Common authentication token data transfer object.
	/// </summary>
	public class TokenDto
	{
		/// <summary>
		/// Gets or sets access token value.
		/// </summary>
		public string AccessToken { get; set; }

		/// <summary>
		/// Gets or sets access token expiration time value.
		/// </summary>
		public DateTime AccessTokenExpire { get; set; }

		/// <summary>
		/// Gets or sets refresh token value.
		/// </summary>
		public string RefreshToken { get; set; }

		/// <summary>
		/// Gets or sets refresh token expiration time value.
		/// </summary>
		public DateTime RefreshTokenExpire { get; set; }
	}
}