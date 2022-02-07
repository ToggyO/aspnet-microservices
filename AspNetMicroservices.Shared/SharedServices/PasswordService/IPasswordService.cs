namespace AspNetMicroservices.Shared.SharedServices.PasswordService
{
	/// <summary>
	/// Represents password management service.
	/// </summary>
	public interface IPasswordService
	{
		/// <summary>
		/// Creates an object with hash, computed from password with generated salt.
		/// </summary>
		/// <param name="password">Provided password string.</param>
		/// <returns></returns>
		PasswordModel CreatePassword(string password);

		/// <summary>
		/// Creates an object with hash, computed from password with provided salt.
		/// </summary>
		/// <param name="password">Provided password string.</param>
		/// <param name="salt">Provided salt string.</param>
		/// <returns></returns>
		PasswordModel CreatePassword(string password, string salt);

		/// <summary>
		/// Verify password string by provided current password model.
		/// </summary>
		/// <param name="currentPassword">Current password model.</param>
		/// <param name="verifyPassword">Password string to be verified.</param>
		/// <returns></returns>
		bool VerifyPassword(PasswordModel currentPassword, string verifyPassword);

		/// <summary>
		/// Generates random salt string.
		/// </summary>
		/// <returns></returns>
		string GenerateSalt();
	}
}