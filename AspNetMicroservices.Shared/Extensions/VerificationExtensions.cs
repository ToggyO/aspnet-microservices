using System.Threading.Tasks;

using AspNetMicroservices.Shared.Exceptions;
using AspNetMicroservices.Shared.Models.Response;

using Grpc.Core;

// TODO: check
namespace AspNetMicroservices.Shared.Extensions
{
	/// <summary>
	/// Verification methods.
	/// </summary>
	public static class VerificationExtensions
	{
		/// <summary>
		/// Checks provided item is not null.
		/// </summary>
		/// <typeparam name="TItem">Item type.</typeparam>
		/// <param name="item">Item to be checked for null.</param>
		/// <returns>Provided item.</returns>
		public static async Task<TItem> EnsureExists<TItem>(this Task<TItem> item)
			where TItem : class
		{
			var value = await item;
			return value.EnsureExists();
		}

		/// <summary>
		/// Checks provided item is not null.
		/// </summary>
		/// <typeparam name="TItem">Item type.</typeparam>
		/// <param name="value">Item to be checked for null.</param>
		/// <returns>Provided item.</returns>
		public static TItem EnsureExists<TItem>(this TItem value)
		{
			if (value == null)
				throw new ErrorResponseRpcException(StatusCode.NotFound, new NotFoundErrorResponse());
			return value;
		}

		/// <summary>
		/// Check Value to be true.
		/// </summary>
		/// <param name="value">Value.</param>
		public static void EnsureTrue(this bool value)
		{
			if (!value)
				throw new ErrorResponseRpcException(StatusCode.InvalidArgument, new BadParametersErrorResponse());
		}

		/// <summary>
		/// Check Value to be true.
		/// </summary>
		/// <param name="value">Value.</param>
		/// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		public static async Task EnsureTrue(this Task<bool> value)
		{
			var item = await value;
			item.EnsureTrue();
		}
	}
}