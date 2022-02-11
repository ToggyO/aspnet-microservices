using AspNetMicroservices.Abstractions.Contracts;

namespace AspNetMicroservices.Products.Business.Features.Products.Models
{
	/// <summary>
	/// Product model.
	/// </summary>
    public class ProductModel : CreateProductModel, IHaveIdentifier
    {
	    /// <inheritdoc cref="IHaveIdentifier"/>.
        public int Id { get; set; }
    }
}