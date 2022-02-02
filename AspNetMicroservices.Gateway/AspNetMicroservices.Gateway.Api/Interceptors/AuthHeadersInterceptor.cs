using AspNetMicroservices.Shared.Constants.Http;

using Grpc.Core;
using Grpc.Core.Interceptors;

using Microsoft.AspNetCore.Http;

namespace AspNetMicroservices.Gateway.Api.Interceptors
{
	// TODO: add description.
	public class AuthHeadersInterceptor : Interceptor
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public AuthHeadersInterceptor(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
			TRequest request, ClientInterceptorContext<TRequest, TResponse> context,
			AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
		{

			var rawToken = _httpContextAccessor.HttpContext?
				.Request
				.Headers[HttpHeaderNames.Authorization];

			var metadata = new Metadata
			{
				{ HttpHeaderNames.Authorization, rawToken },
			};

			var callOptions = context.Options.WithHeaders(metadata);
			context = new ClientInterceptorContext<TRequest, TResponse>(
				context.Method, context.Host, callOptions);

			return base.AsyncUnaryCall(request, context, continuation);
		}
	}
}