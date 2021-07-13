using LinqToDB.Mapping;

namespace AspNetMicroservices.Products.DataLayer.Entities.Product
{
	/// <summary>
	/// Products table entity.
	/// </summary>
    [Table("dat_products")]
    public class ProductEntity : BaseEntity
    {
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