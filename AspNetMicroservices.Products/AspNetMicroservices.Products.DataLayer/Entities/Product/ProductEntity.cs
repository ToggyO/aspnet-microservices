using AspNetMicroservices.Abstractions.Contracts;

using LinqToDB.Mapping;

namespace AspNetMicroservices.Products.DataLayer.Entities.Product
{
	/// <summary>
	/// Products table entity.
	/// </summary>
    [Table("dat_products")]
    public class ProductEntity : BaseEntity, IHaveIdentifier
    {
	    /// <summary>
	    /// Item unique identity.
	    /// </summary>
	    [PrimaryKey, Identity, Column(Name = "id")]
	    public int Id { get; set; }

	    /// <summary>
	    /// Product name.
	    /// </summary>
	    [Column(Name = "name"), NotNull]
        public string Name { get; set; }

        /// <summary>
        /// Product description.
        /// </summary>
	    [Column(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Product price.
        /// </summary>
        [Column(Name = "price"), NotNull]
        public long Price { get; set; }
    }
}