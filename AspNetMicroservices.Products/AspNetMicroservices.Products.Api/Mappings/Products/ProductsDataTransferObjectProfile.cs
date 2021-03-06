using AspNetMicroservices.Grpc.Protos.Products;
using AspNetMicroservices.Products.Business.Features.Products.Commands;

using AutoMapper;

namespace AspNetMicroservices.Products.Api.Mappings.Products
{
	/// <summary>
	/// Products data transfer object mapper profile.
	/// </summary>
	public class ProductsDataTransferObjectProfile : Profile
	{
		/// <summary>
		/// Initialize new instance of <see cref="ProductsDataTransferObjectProfile"/>.
		/// </summary>
		public ProductsDataTransferObjectProfile()
		{
			CreateMap<CreateProductDto, CreateProduct.Command>();
			CreateMap<ProductDto, UpdateProduct.Command>();
		}
	}
}