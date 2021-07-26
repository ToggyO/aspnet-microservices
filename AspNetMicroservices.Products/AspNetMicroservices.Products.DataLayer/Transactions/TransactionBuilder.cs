using System.Threading.Tasks;

using AspNetMicroservices.Shared.Contracts;

using LinqToDB.Data;

namespace AspNetMicroservices.Products.DataLayer.Transactions
{
	/// <inheritdoc cref="ITransactional{TTransactionProvider}"/>
	public class TransactionBuilder : ITransactional<DataConnectionTransaction>
	{
		/// <summary>
		/// Instance of database connection <see cref="DataConnection"/>.
		/// </summary>
		private readonly DataConnection _connection;

		/// <summary>
		/// Initialize new instance of <see cref="TransactionBuilder"/>.
		/// </summary>
		/// <param name="connection">Database connection.</param>
		public TransactionBuilder(DataConnection connection)
			=> _connection = connection;

		/// <inheritdoc cref="ITransactional{TTransactionProvider}.CreateTransactionAsync"/>.
		public async Task<DataConnectionTransaction> CreateTransactionAsync()
			=> await _connection.BeginTransactionAsync();
	}
}