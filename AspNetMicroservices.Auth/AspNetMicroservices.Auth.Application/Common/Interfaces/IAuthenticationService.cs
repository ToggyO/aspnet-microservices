using System.Threading.Tasks;

using AspNetMicroservices.Auth.Application.Dto.Users;
using AspNetMicroservices.Auth.Domain.Models.Database.Users;
using AspNetMicroservices.Shared.Models.Auth;

namespace AspNetMicroservices.Auth.Application.Common.Interfaces
{
    /// <summary>
    /// Authentication process handler.
    /// </summary>
    public interface IAuthenticationService
    {
	    /// <summary>
	    /// Handle authentication process for provided user.
	    /// </summary>
	    /// <param name="user">User model.</param>
	    /// <returns></returns>
        Task<AuthenticationTicket<UserDto>> HandleAuthentication(UserModel user);
    }
}