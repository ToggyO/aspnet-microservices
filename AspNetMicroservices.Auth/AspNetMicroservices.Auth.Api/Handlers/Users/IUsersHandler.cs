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
		/// <param name="cmd">New user data.</param>
		/// <returns></returns>
		Task<Response<UserDto>> CreateUser(CreateUser.Command cmd);

		/// <summary>
		/// Get user by identifier.
		/// </summary>
		/// <param name="id">User identifier.</param>
		/// <returns></returns>
		Task<Response<UserDto>> GetById(int id);
	}
}