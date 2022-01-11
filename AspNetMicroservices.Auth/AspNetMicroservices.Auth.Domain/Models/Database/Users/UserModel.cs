using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using AspNetMicroservices.Shared.Contracts;

namespace AspNetMicroservices.Auth.Domain.Models.Database.Users
{
	/// <summary>
	/// Represents user model.
	/// </summary>
	[Table("users")]
	public class UserModel : BaseModel, IPortalUser
	{
		/// <inheritdoc cref="IHaveIdentifier.Id"/>.
		[Key, Column("id")]
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
		/// Password hash.
		/// </summary>
		[Column("hash")]
		public string Hash { get; set; }

		/// <summary>
		/// Password salt.
		/// </summary>
		[Column("salt")]
		public string Salt { get; set; }

		/// <summary>
		/// Detailed information about user.
		/// </summary>
		public UserDetailModel Details { get; set; }
	}
}