using System.Threading.Tasks;

using AspNetMicroservices.Products.DataLayer.DataBase.AppDataConnection;
using AspNetMicroservices.Shared.Contracts;

using LinqToDB.Data;

namespace AspNetMicroservices.Products.DataLayer.Transactions
{
	/// <inheritdoc cref="ITransactional{TTransactionProvider}"/>
	public class TransactionBuilder : IAsyncTransactional<DataConnectionTransaction>
	{
		/// <summary>
		/// Instance of database connection <see cref="AppDataConnection"/>.
		/// </summary>
		protected readonly AppDataConnection Connection;

		/// <summary>
		/// Initialize new instance of <see cref="TransactionBuilder"/>.
		/// </summary>
		/// <param name="connection">Database connection.</param>
		public TransactionBuilder(AppDataConnection connection)
			=> Connection = connection;

		/// <inheritdoc cref="ITransactional{TTransactionProvider}.CreateTransactionAsync"/>.
		public async Task<DataConnectionTransaction> CreateTransactionAsync()
			=> await Connection.BeginTransactionAsync();
	}
}