using System.Threading.Tasks;

using AspNetMicroservices.Auth.Application.Dto.Users;
using AspNetMicroservices.Auth.Application.Features.Users.Commands;
using AspNetMicroservices.Shared.Models.Response;

namespace AspNetMicroservices.Auth.Api.Handlers.Users
{
	/// <summary>
	/// Http request handler for user entity.
	/// </summary>
	public interface IUsersHandler
	{
		/// <summary>
		/// Creates user.
		/// </summary>
		/// <param name="dto">New user data.</param>
		/// <returns></returns>
		Task<Response<UserDto>> CreateUser(CreateUser.Command cmd);
	}
}