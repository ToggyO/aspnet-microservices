using System.Data;
using System.Threading.Tasks;

using AspNetMicroservices.Auth.DataAccess.Context;
using AspNetMicroservices.Auth.Domain.Models.Database;
using AspNetMicroservices.Shared.Contracts;

using Npgsql;

namespace AspNetMicroservices.Auth.DataAccess.Transactions
{
	/// <inheritdoc cref="ITransactional{TTransactionProvider}"/>
	public class TransactionBuilder : ITransactional<IDbTransaction>
	{
		private readonly AuthServiceDbContext _connectionFactory;

		public TransactionBuilder(AuthServiceDbContext context)
		{
			_connectionFactory = context;
		}

		public async Task<IDbTransaction> CreateTransactionAsync()
			=> await _connectionFactory.GetDbConnection().BeginTransactionAsync();
	}
}