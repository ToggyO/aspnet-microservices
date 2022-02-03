using System.Threading.Tasks;

using AspNetMicroservices.Auth.Api.Handlers.Users;
using AspNetMicroservices.Auth.Application.Dto.Users;
using AspNetMicroservices.Auth.Application.Features.Users.Commands;
using AspNetMicroservices.Shared.Models.Response;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetMicroservices.Auth.Api.Controllers.v1
{
	[ApiController]
	[Route("users")]
	public class UsersController : ControllerBase
	{
		private readonly IUsersHandler _handler;

		public UsersController(IUsersHandler handler)
		{
			_handler = handler;
		}

		// [HttpPost("list")]
		// public async Task<PaginationModel<UserModel>> GetList([FromBody] QueryFilterModel filter)
		// 	=> await _repository.GetList(filter);

		[HttpGet("{id}")]
		public async Task<Response<UserDto>> GetById([FromRoute] int id) => await _handler.GetById(id);

		[AllowAnonymous]
		[HttpPost("sign-up")]
		public async Task<Response<UserDto>> SignUp([FromBody] CreateUser.Command cmd)
			=> await _handler.CreateUser(cmd);
	}
}