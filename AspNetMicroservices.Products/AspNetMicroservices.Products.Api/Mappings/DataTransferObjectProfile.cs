using AspNetMicroservices.Shared.Constants.Common;
using AspNetMicroservices.Shared.Models.QueryFilter.Implementation;
using AspNetMicroservices.Shared.Protos.Common;

using AutoMapper;

namespace AspNetMicroservices.Products.Api.Mappings
{
	/// <summary>
	/// Query filter mapper profile.
	/// </summary>
	public class DataTransferObjectProfile : Profile
	{
		/// <summary>
		/// Creates an instance of <see cref="DataTransferObjectProfile"/>.
		/// </summary>
		public DataTransferObjectProfile()
		{
			CreateMap<QueryFilterRequest, QueryFilterModel>()
				.ForMember(x => x.Page, opts => opts
					.MapFrom((src, dest)
						=> dest.Page = src.Page == 0 ? PaginationDefaults.DefaultPageNumber : src.Page))
				.ForMember(x => x.PageSize, opts => opts
					.MapFrom((src, dest)
						=> dest.PageSize = src.PageSize == 0 ? PaginationDefaults.DefaultPageSize : src.PageSize));
		}
	}
}