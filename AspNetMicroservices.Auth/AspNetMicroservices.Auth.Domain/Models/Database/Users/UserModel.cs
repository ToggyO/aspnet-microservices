using AspNetMicroservices.Shared.Contracts;

namespace AspNetMicroservices.Auth.Domain.Models.Database.Users
{
	/// <summary>
	/// Represents user model.
	/// </summary>
	public class UserModel : BaseModel, IHaveIdentifier
	{
		/// <inheritdoc cref="IHaveIdentifier.Id"/>.
		public int Id { get; set; }

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
		/// Detailed information about user.
		/// </summary>
		public UserDetailModel Details { get; set; }

		public const string BaseQuery = "SELECT u.id, u.first_name, u.last_name, u.email, u.created_at, u.updated_at FROM users u ";
	}
}