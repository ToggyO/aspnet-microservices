using System.Threading;
using System.Threading.Tasks;

using AspNetMicroservices.Auth.Application.Dto.Users;
using AspNetMicroservices.Auth.Domain.Repositories;
using AspNetMicroservices.Shared.Contracts;

using MapsterMapper;

using MediatR;

namespace AspNetMicroservices.Auth.Application.Features.Users.Queries
{
    /// <summary>
    /// Get user by identifier.
    /// </summary>
    public class GetUserById
    {
	    /// <summary>
	    /// Get user by identifier query.
	    /// </summary>
        public class Query : IHaveIdentifier, IRequest<UserDto>
        {
            /// <inheritdoc cref="IHaveIdentifier.Id"/>.
            public int Id { get; set; }
        }

		/// <summary>
		/// Get user by identifier query handler.
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
				var user = await _repository.GetById(query.Id);
                if (user is null)
					return default;
				return _mapper.From(user).AdaptToType<UserDto>();
			}
		}
	}
}