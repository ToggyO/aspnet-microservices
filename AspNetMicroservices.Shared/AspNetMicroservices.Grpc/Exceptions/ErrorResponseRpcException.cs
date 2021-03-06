using System.Text.Json;

using AspNetMicroservices.Abstractions.Models.Response;

using Grpc.Core;

namespace AspNetMicroservices.Grpc.Exceptions
{
	/// <summary>
	/// Represents typed instance of <see cref="RpcException"/>.
	/// </summary>
	/// <typeparam name="TItem">Represents the type of data returned.</typeparam>
    public class ErrorResponseRpcException<TItem> : RpcException where TItem : class
    {
	    /// <summary>
	    /// Creates an instance of <see cref="ErrorResponseRpcException{TItem}"/> associated with
	    /// the given status code and error response data transfer object
	    /// </summary>
	    /// <param name="statusCode">Result of a remote procedure call.</param>
	    /// <param name="errorResponse">Instance of <see cref="ErrorResponse"/>.</param>
        public ErrorResponseRpcException(StatusCode statusCode, ErrorResponse<TItem> errorResponse)
            : base(new Status(statusCode, JsonSerializer.Serialize(errorResponse)))
        {}
    }

	/// <summary>
	/// Represents typed instance of <see cref="RpcException"/>.
	/// </summary>
	public class ErrorResponseRpcException : RpcException
	{
		/// <summary>
		/// Creates an instance of <see cref="ErrorResponseRpcException{TItem}"/> associated with
		/// the given status code and error response data transfer object
		/// </summary>
		/// <param name="statusCode">Result of a remote procedure call.</param>
		/// <param name="errorResponse">Instance of <see cref="ErrorResponse{TItem}"/>.</param>
		public ErrorResponseRpcException(StatusCode statusCode, ErrorResponse errorResponse)
			: base(new Status(statusCode, JsonSerializer.Serialize(errorResponse)))
		{}
	}
}
