using System.Threading.Tasks;

using AspNetMicroservices.Abstractions.Models.Response;
using AspNetMicroservices.Auth.Application.Dto.Users;
using AspNetMicroservices.Auth.Application.Features.Users.Commands;
using AspNetMicroservices.Auth.Application.Features.Users.Queries;

using MediatR;

namespace AspNetMicroservices.Auth.Api.Handlers.Users.Implementation
{
	/// <inheritdoc cref="IUsersHandler"/>.
	public class UsersHandler : IUsersHandler
	{
		/// <summary>
		/// Instance of <see cref="IMediator"/>.
		/// </summary>
		private readonly IMediator _mediator;

		/// <summary>
		/// Initialize new instance of <see cref="UsersHandler"/>.
		/// </summary>
		/// <param name="mediator">Application mediator.</param>
		public UsersHandler(IMediator mediator)
		{
			_mediator = mediator;
		}

		/// <inheritdoc cref="IUsersHandler.CreateUser"/>.
		public async Task<Response<UserDto>> CreateUser(CreateUser.Command cmd)
		{
			var user = await _mediator.Send(new GetUserByEmail.Query { Email = cmd.Email });
			if (user is not null)
				return new BusinessConflictErrorResponse<UserDto>();

			return new Response<UserDto> { Data = await _mediator.Send(cmd) };
		}

		/// <inheritdoc cref="IUsersHandler.GetById"/>.
		public async Task<Response<UserDto>> GetById(int id)
		{
			var user = await _mediator.Send(new GetUserById.Query { Id = id });
			if (user is null)
				return new NotFoundErrorResponse<UserDto>();

			return new Response<UserDto> { Data = user };
		}
	}
}