using AspNetMicroservices.Grpc.Protos.Products;
using AspNetMicroservices.Products.Business.Features.Products.Models;
using AspNetMicroservices.Products.DataLayer.Entities.Product;

using AutoMapper;

namespace AspNetMicroservices.Products.Business.Features.Products.Mappings
{
	/// <summary>
	/// Products mapper profile.
	/// </summary>
	public class ProductsMapperProfile : Profile
	{
		/// <summary>
		/// Creates an instance of <see cref="ProductsMapperProfile"/>.
		/// </summary>
		public ProductsMapperProfile()
		{
			CreateMap<ProductEntity, ProductModel>().ReverseMap();
			CreateMap<ProductModel, ProductDto>().ReverseMap();
		}
	}
}