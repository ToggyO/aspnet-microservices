using System.Threading.Tasks;

namespace AspNetMicroservices.Shared.Contracts
{
	/// <summary>
	/// Represents an object for creating and managing database transactions.
	/// </summary>
	/// <typeparam name="TTransactionExecutor"></typeparam>
	public interface ITransactional<TTransactionExecutor>
	{
		/// <summary>
		/// Starts transaction.
		/// </summary>
		/// <returns>Transaction object.</returns>
		Task<TTransactionExecutor> CreateTransactionAsync();
	}
}