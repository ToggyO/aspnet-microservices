using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

using AspNetMicroservices.Auth.Application.Dto.Users;
using AspNetMicroservices.Auth.Application.Features.Users.Validators;
using AspNetMicroservices.Auth.Domain.Models.Database.Users;
using AspNetMicroservices.Auth.Domain.Repositories;

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
		public class Command : CreateUpdateUserDto, IRequest<UserModel>
		{}

		/// <summary>
		/// Create user command handler.
		/// </summary>
		public class Handler : IRequestHandler<Command, UserModel>
		{
			/// <summary>
			/// Instance of <see cref="IUsersRepository"/>.
			/// </summary>
			private readonly IUsersRepository _repository;

			/// <summary>
			/// Initialize new instance of <see cref="Handler"/>.
			/// </summary>
			/// <param name="repository">Users repository.</param>
			public Handler(IUsersRepository repository)
			{
				_repository = repository;
			}

			/// <inheritdoc cref="IRequestHandler{TRequest,TResponse}.Handle"/>.
			public async Task<UserModel> Handle(Command request, CancellationToken cancellationToken)
			{
				using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);;
				var user = await _repository.Create(user);
				user.Details.UserId = user.Id;
				user.Details = await _repository.CreateDetails(user.Details);
				scope.Complete();
				return user;
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