using AspNetMicroservices.Auth.Application.Dto.Users;
using AspNetMicroservices.Auth.Domain.Models.Database.Users;

using Mapster;

namespace AspNetMicroservices.Auth.Application.Mappings.Registry
{
	/// <summary>
	/// Users mapper profile.
	/// </summary>
	public class UserMappingRegistry : IRegister
	{
		public void Register(TypeAdapterConfig config)
		{
			config.NewConfig<CreateUpdateUserDto, UserModel>()
				.Map(dest => dest.Details,
					src => new UserDetailModel
					{
						Address = src.Address,
						PhoneNumber = src.PhoneNumber
					});

			config.NewConfig<UserModel, UserDto>()
				.Map(desc => desc.Details,
					src => new UserDetailDto
					{
						Id = src.Details.Id,
						Address = src.Details.Address,
						PhoneNumber = src.Details.PhoneNumber,
						UserId = src.Details.UserId
					})
				.TwoWays();

			config.NewConfig<UserDetailModel, UserDetailDto>().TwoWays();
		}
	}
}