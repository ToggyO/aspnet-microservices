using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

using AspNetMicroservices.Auth.Application.Dto.Users;
using AspNetMicroservices.Auth.Application.Features.Users.Validators;
using AspNetMicroservices.Auth.Domain.Models.Database.Users;
using AspNetMicroservices.Auth.Domain.Repositories;
using AspNetMicroservices.Shared.Models.Response;
using AspNetMicroservices.Shared.SharedServices.PasswordService;

using MapsterMapper;

using MediatR;

namespace AspNetMicroservices.Auth.Application.Features.Users.Commands
{
	/// <summary>
	/// Create user.
	/// </summary>
	public class CreateUser
	{
		/// <summary>
		/// Create user command.
		/// </summary>
		public class Command : CreateUpdateUserDto, IRequest<UserDto>
		{}

		/// <summary>
		/// Create user command handler.
		/// </summary>
		public class Handler : IRequestHandler<Command, UserDto>
		{
			/// <summary>
			/// Instance of <see cref="IUsersRepository"/>.
			/// </summary>
			private readonly IUsersRepository _repository;

			/// <summary>
			/// Password management service.
			/// </summary>
			private readonly IPasswordService _passwordService;

			/// <summary>
			/// Instance of <see cref="IMapper"/>.
			/// </summary>
			private readonly IMapper _mapper;

			/// <summary>
			/// Initialize new instance of <see cref="Handler"/>.
			/// </summary>
			/// <param name="repository">Users repository.</param>
			/// <param name="passwordService">Instance of <see cref="IPasswordService"/>.</param>
			/// <param name="mapper">Object mapper.</param>
			public Handler(IUsersRepository repository,
				IPasswordService passwordService,
				IMapper mapper)
			{
				_repository = repository;
				_passwordService = passwordService;
				_mapper = mapper;
			}

			/// <inheritdoc cref="IRequestHandler{TRequest,TResponse}.Handle"/>.
			public async Task<UserDto> Handle(Command cmd, CancellationToken cancellationToken)
			{
				using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

				var userEntity = _mapper.From(cmd).AdaptToType<UserModel>();

				var passwordModel = _passwordService.CreatePassword(cmd.Password);
				userEntity.Salt = passwordModel.Salt;
				userEntity.Hash = passwordModel.Hash;

				var user = await _repository.Create(userEntity);
				user.Details.UserId = user.Id;
				user.Details = await _repository.CreateDetails(user.Details);

				scope.Complete();
				return _mapper.From(user).AdaptToType<UserDto>();
			}
		}

		#region Validator

		/// <summary>
		/// Create user command validator.
		/// </summary>
		public class Validator : CreateUpdateUserValidator<Command>
		{}

		#endregion
	}
}