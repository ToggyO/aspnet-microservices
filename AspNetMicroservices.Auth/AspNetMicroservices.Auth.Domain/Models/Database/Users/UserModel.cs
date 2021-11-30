using System.ComponentModel.DataAnnotations.Schema;

using AspNetMicroservices.Shared.Contracts;

namespace AspNetMicroservices.Auth.Domain.Models.Database.Users
{
	/// <summary>
	/// Represents user model.
	/// </summary>
	[Table("users")]
	public class UserModel : BaseModel, IHaveIdentifier
	{
		/// <inheritdoc cref="IHaveIdentifier.Id"/>.
		[Column("id")]
		public int Id { get; set; }

		/// <summary>
		/// First name.
		/// </summary>
		[Column("first_name")]
		public string FirstName { get; set; }

		/// <summary>
		/// Last name.
		/// </summary>
		[Column("last_name")]
		public string LastName { get; set; }

		/// <summary>
		/// Email.
		/// </summary>
		[Column("email")]
		public string Email { get; set; }

		/// <summary>
		/// Hashed password.
		/// </summary>
		[Column("password")]
		public string Password { get; set; }

		/// <summary>
		/// Detailed information about user.
		/// </summary>
		public UserDetailModel Details { get; set; }

		public const string BaseQuery = "SELECT u.id, u.first_name, u.last_name, u.email, u.created_at, u.updated_at FROM users u ";
	}
}