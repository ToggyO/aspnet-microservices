namespace AspNetMicroservices.SharedServices.PasswordService
{
	/// <summary>
	/// Encrypted password model.
	/// </summary>
	public class PasswordModel
	{
		/// <summary>
		/// Salt.
		/// </summary>
		public string Salt { get; set; }

		/// <summary>
		/// Password hash.
		/// </summary>
		public string Hash { get; set; }
	}
}