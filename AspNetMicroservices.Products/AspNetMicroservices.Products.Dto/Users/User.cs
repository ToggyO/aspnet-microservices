using AspNetMicroservices.Abstractions.Models.PortalUser;

namespace AspNetMicroservices.Products.Dto.Users
{
	public class User : PortalUser<UserDetails> {}

	public class UserDetails : PortalUserDetails {}
}