using System.Threading.Tasks;
using System.Transactions;

using AspNetMicroservices.Auth.Application.Dto.Users;
using AspNetMicroservices.Auth.Application.Features.Users.Commands;
using AspNetMicroservices.Auth.Domain.Models.Database.Users;
using AspNetMicroservices.Auth.Domain.Repositories;
using AspNetMicroservices.Shared.Models.Pagination;
using AspNetMicroservices.Shared.Models.QueryFilter.Implementation;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace AspNetMicroservices.Auth.Api.Controllers.v1
{
	[ApiController]
	[Route("users")]
	public class UsersController : ControllerBase
	{
		private readonly IUsersRepository _repository;

		private readonly IMediator _mediator;

		public UsersController(IUsersRepository repository, IMediator mediator)
		{
			_repository = repository;
			_mediator = mediator;
		}

		[HttpPost("list")]
		public async Task<PaginationModel<UserModel>> GetList([FromBody] QueryFilterModel filter)
			=> await _repository.GetList(filter);

		[HttpGet("{id}")]
		public async Task<UserModel> GetById([FromRoute] int id) => await _repository.GetById(id);

		[HttpPost]
		public async Task<UserModel> Create([FromBody] CreateUser.Command cmd) => await _mediator.Send(cmd);

	}
}