namespace AspNetMicroservices.Auth.Application.Dto.Users
{
	/// <summary>
	/// Create user data transfer object
	/// </summary>
	public class CreateUpdateUserDto
	{
		/// <summary>
		/// First name.
		/// </summary>
		public string FirstName { get; set; }

		/// <summary>
		/// Last name.
		/// </summary>
		public string LastName { get; set; }

		/// <summary>
		/// Email.
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// Hashed password.
		/// </summary>
		public string Password { get; set; }

		/// <summary>
		/// Living address.
		/// </summary>
		public string Address { get; set; }

		/// <summary>
		/// Phone number.
		/// </summary>
		public string PhoneNumber { get; set; }
	}
}