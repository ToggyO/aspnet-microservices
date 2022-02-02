using System;
using System.Text.Json;
using System.Threading.Tasks;

using Grpc.Core;
using Grpc.Core.Interceptors;

namespace AspNetMicroservices.Gateway.Api.Interceptors
{
	/// <summary>
	/// Rpc error interceptor.
	/// </summary>
    public class RpcErrorInterceptor : Interceptor
    {
        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
            TRequest request,
            ClientInterceptorContext<TRequest, TResponse> context,
            AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            var call = continuation(request, context);
            return new AsyncUnaryCall<TResponse>(
                HandleResponse(call.ResponseAsync),
                call.ResponseHeadersAsync,
                call.GetStatus,
                call.GetTrailers,
                call.Dispose);
        }

        private async Task<TResponse> HandleResponse<TResponse>(Task<TResponse> t)
        {
            try
            {
                return await t;
            }
            catch (RpcException e)
            {
                // var error = JsonSerializer.Deserialize(e.Message)
                throw new RpcException(e.Status);
            }
        }
    }
}