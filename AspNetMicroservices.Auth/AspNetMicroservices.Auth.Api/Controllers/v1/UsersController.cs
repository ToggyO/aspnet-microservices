using System.Threading.Tasks;
using System.Transactions;

using AspNetMicroservices.Auth.Api.Handlers.Users;
using AspNetMicroservices.Auth.Application.Dto.Users;
using AspNetMicroservices.Auth.Application.Features.Users.Commands;
using AspNetMicroservices.Auth.Domain.Models.Database.Users;
using AspNetMicroservices.Auth.Domain.Repositories;
using AspNetMicroservices.Shared.Models.Pagination;
using AspNetMicroservices.Shared.Models.QueryFilter.Implementation;
using AspNetMicroservices.Shared.Models.Response;

using MediatR;

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
		public Task<UserModel> GetById([FromRoute] int id) => Task.FromResult(new UserModel { Id = id });

		[AllowAnonymous]
		[HttpPost("sign-up")]
		public async Task<Response<UserDto>> SignUp([FromBody] CreateUser.Command cmd)
			=> await _handler.CreateUser(cmd);
	}
}