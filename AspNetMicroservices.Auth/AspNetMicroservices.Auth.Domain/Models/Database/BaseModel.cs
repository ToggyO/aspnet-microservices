using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetMicroservices.Auth.Domain.Models.Database
{
	/// <summary>
	/// Represents base mode with set of common properties.
	/// </summary>
	public class BaseModel
	{
		/// <summary>
		/// Entity created timestamp.
		/// </summary>
		[Column("created_at")]
		public DateTime CreatedAt { get; set; } = DateTime.Now;

		/// <summary>
		/// Entity updated timestamp.
		/// </summary>
		[Column("updated_at")]
		public DateTime UpdatedAt { get; set; } = DateTime.Now;
	}
}