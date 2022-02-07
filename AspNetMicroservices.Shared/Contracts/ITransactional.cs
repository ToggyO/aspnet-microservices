using System.Threading.Tasks;

namespace AspNetMicroservices.Shared.Contracts
{
	/// <summary>
	/// Represents an object for creating and managing database transactions.
	/// </summary>
	/// <typeparam name="TTransactionProvider">Type of transaction provider.</typeparam>
	public interface ITransactional<out TTransactionProvider>
	{
		/// <summary>
		/// Starts transaction.
		/// </summary>
		/// <returns>Transaction object.</returns>
		TTransactionProvider CreateTransaction();
	}
}

