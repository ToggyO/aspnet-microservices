using System.Threading;
using System.Threading.Tasks;

using AspNetMicroservices.Auth.Application.Dto.Users;
using AspNetMicroservices.Auth.Domain.Repositories;

using MapsterMapper;

using MediatR;

namespace AspNetMicroservices.Auth.Application.Features.Users.Queries
{
	/// <summary>
	/// Retrieve user by email.
	/// </summary>
	public class GetUserByEmail
	{
		/// <summary>
		/// Retrieve user by email query.
		/// </summary>
		public class Query : IRequest<UserDto>
		{
			/// <summary>
			/// User email.
			/// </summary>
			public string Email { get; set; }
		}

		/// <summary>
		/// Retrieve user by email handler.
		/// </summary>
		public class Handler : IRequestHandler<Query, UserDto>
		{
			/// <summary>
			/// Instance of <see cref="IUsersRepository"/>.
			/// </summary>
			private readonly IUsersRepository _repository;

			/// <summary>
			/// Instance of <see cref="IMapper"/>.
			/// </summary>
			private readonly IMapper _mapper;

			/// <summary>
			/// Initialize new instance of <see cref="Handler"/>.
			/// </summary>
			/// <param name="repository">Users repository.</param>
			/// <param name="mapper">Object mapper.</param>
			public Handler(IUsersRepository repository, IMapper mapper)
			{
				_repository = repository;
				_mapper = mapper;
			}

			/// <inheritdoc cref="IRequestHandler{TRequest,TResponse}.Handle"/>.
			public async Task<UserDto> Handle(Query query, CancellationToken cancellationToken)
			{
				var user = await _repository.GetByEmail(query.Email);
				if (user is null)
					return default;
				return _mapper.From(user).AdaptToType<UserDto>();
			}
		}
	}
}