using System;

using LinqToDB;
using LinqToDB.Common;
using LinqToDB.Mapping;

namespace AspNetMicroservices.Products.DataLayer.Entities
{
	/// <summary>
	/// Represents base LinqToDB entity with set of common properties.
	/// </summary>
    public abstract class BaseEntity
    {
	    /// <summary>
	    /// Entity created timestamp.
	    /// </summary>
        [Column("created_at", SkipOnUpdate = true)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

	    /// <summary>
	    /// Entity updated timestamp.
	    /// </summary>
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}