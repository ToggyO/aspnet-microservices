using AspNetMicroservices.Shared.Models.PortalUser;

namespace AspNetMicroservices.Auth.Application.Dto.Users
{
	/// <summary>
	/// User data transfer object.
	/// </summary>
	public class UserDto : PortalUser<UserDetailDto> {}
}