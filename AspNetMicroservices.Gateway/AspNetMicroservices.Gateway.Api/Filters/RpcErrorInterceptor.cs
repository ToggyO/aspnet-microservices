using System;
using System.Threading.Tasks;

using Grpc.Core;
using Grpc.Core.Interceptors;

namespace AspNetMicroservices.Gateway.Api.Filters
{
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
                throw new RpcException(new Status(e.StatusCode, e.Message));
            }
        }
    }
}