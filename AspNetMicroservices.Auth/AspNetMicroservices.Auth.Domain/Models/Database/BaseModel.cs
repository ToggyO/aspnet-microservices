using System;

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
		public DateTime CreatedAt { get; set; } = DateTime.Now;

		/// <summary>
		/// Entity updated timestamp.
		/// </summary>
		public DateTime UpdatedAt { get; set; } = DateTime.Now;
	}
}