using System.Threading.Tasks;

using AspNetMicroservices.Auth.Application.Dto.Users;
using AspNetMicroservices.Auth.Domain.Models.Database.Users;
using AspNetMicroservices.Auth.Domain.Repositories;
using AspNetMicroservices.Shared.Models.Pagination;
using AspNetMicroservices.Shared.Models.QueryFilter.Implementation;

using Microsoft.AspNetCore.Mvc;

namespace AspNetMicroservices.Auth.Api.Controllers.v1
{
	[ApiController]
	[Route("users")]
	public class UsersController : ControllerBase
	{
		private readonly IUsersRepository _repository;

		public UsersController(IUsersRepository repository)
		{
			_repository = repository;
		}

		[HttpPost("list")]
		public async Task<PaginationModel<UserModel>> GetList([FromBody] QueryFilterModel filter)
			=> await _repository.GetList(filter);

		[HttpGet("{id}")]
		public async Task<UserModel> GetById([FromRoute] int id) => await _repository.GetById(id);

		[HttpPost]
		public async Task<UserModel> Create([FromBody] CreateUserDto dto)
		{
			var user = new UserModel
			{
				FirstName = dto.FirstName,
				LastName = dto.LastName,
				Email = dto.Email,
				Password = dto.Password,
				Details = new UserDetailModel
				{
					Address = dto.Address,
					PhoneNumber = dto.PhoneNumber,
				}
			};
			return await _repository.Create(user);
		}
	}
}