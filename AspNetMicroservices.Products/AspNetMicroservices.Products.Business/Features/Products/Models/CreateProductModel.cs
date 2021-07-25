namespace AspNetMicroservices.Products.Business.Features.Products.Models
{
	/// <summary>
	/// Create product data transfer object.
	/// </summary>
	public class CreateProductModel
	{
		/// <summary>
        /// Gets or sets the product name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the product description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the product price.
        /// </summary>
        public long Price { get; set; }
	}
}