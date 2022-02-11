using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using AspNetMicroservices.Abstractions.Contracts;

namespace AspNetMicroservices.Auth.Domain.Models.Database.Users
{
	/// <summary>
	/// Detailed information about user.
	/// </summary>
	[Table("user_details")]
	public class UserDetailModel : BaseModel, IHaveIdentifier
	{
		/// <inheritdoc cref="IHaveIdentifier.Id"/>.
		[Key, Column("id")]
		public int Id { get; set; }

		/// <summary>
		/// Living address.
		/// </summary>
		[Column("address")]
		public string Address { get; set; }

		/// <summary>
		/// Phone number.
		/// </summary>
		[Column("phone_number")]
		public string PhoneNumber { get; set; }

		/// <summary>
		/// User identifier.
		/// </summary>
		[Column("user_id")]
		public int UserId { get; set; }
	}
}