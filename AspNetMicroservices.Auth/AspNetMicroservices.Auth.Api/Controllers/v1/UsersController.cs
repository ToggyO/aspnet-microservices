using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;

using AspNetMicroservices.Abstractions.Models.Response;
using AspNetMicroservices.Auth.Api.Handlers.Users;
using AspNetMicroservices.Auth.Application.Dto.Users;
using AspNetMicroservices.Auth.Application.Features.Users.Commands;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetMicroservices.Auth.Api.Controllers.v1
{
	/// <summary>
	/// Http request controller for user entity.
	/// </summary>
	[ApiController]
	[Route("users")]
	public class UsersController : ControllerBase, IUsersHandler
	{
		/// <summary>
		/// Users handler.
		/// </summary>
		private readonly IUsersHandler _handler;

		/// <summary>
		/// Initialize new instance of <see cref="UsersController"/>.
		/// </summary>
		/// <param name="handler">Instance of <see cref="IUsersHandler"/>.</param>
		public UsersController(IUsersHandler handler)
		{
			_handler = handler;
		}

		// [HttpPost("list")]
		// public async Task<PaginationModel<UserModel>> GetList([FromBody] QueryFilterModel filter)
		// 	=> await _repository.GetList(filter);

		/// <inheritdoc cref="IUsersHandler.GetById"/>.
		[HttpGet("{id}")]
		[ProducesResponseType(typeof(Response<UserDto>), (int)HttpStatusCode.OK)]
		[ProducesResponseType(typeof(BusinessConflictErrorResponse), (int)HttpStatusCode.Conflict)]
		public async Task<Response<UserDto>> GetById([FromRoute] int id) => await _handler.GetById(id);

		/// <inheritdoc cref="IUsersHandler.CreateUser"/>.
		[AllowAnonymous]
		[HttpPost("sign-up")]
		[ProducesResponseType(typeof(Response<UserDto>), (int)HttpStatusCode.Created)]
		[ProducesResponseType(typeof(NotFoundErrorResponse), (int)HttpStatusCode.NotFound)]
		public async Task<Response<UserDto>> CreateUser([FromBody] CreateUser.Command cmd)
			=> await _handler.CreateUser(cmd);
	}
}