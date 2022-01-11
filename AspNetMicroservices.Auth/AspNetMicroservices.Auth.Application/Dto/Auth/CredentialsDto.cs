namespace AspNetMicroservices.Auth.Application.Dto.Auth
{
	/// <summary>
	/// Represents authentication credentials data transfer object.
	/// </summary>
	public class CredentialsDto
	{
		/// <summary>
		/// Gets or sets login credential value.
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// Gets or sets password credential value.
		/// </summary>
		public string Password { get; set; }
	}
}