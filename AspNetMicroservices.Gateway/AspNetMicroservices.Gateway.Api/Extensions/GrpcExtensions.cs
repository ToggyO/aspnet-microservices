using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using AspNetMicroservices.Abstractions.Models.Response;
using AspNetMicroservices.Gateway.Api.Interceptors;

using Grpc.Core;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetMicroservices.Gateway.Api.Extensions
{
	/// <summary>
	/// Grpc extensions.
	/// </summary>
    public static class GrpcExtensions
    {
	    /// <summary>
	    /// Method add configured Grpc client to an instance of <see cref="IServiceCollection"/>.
	    /// </summary>
	    /// <param name="services">Instance of <see cref="IServiceCollection"/>.</param>
	    /// <param name="url">Url path to Rpc server.</param>
	    /// <typeparam name="TService">Represents type of Rpc client.</typeparam>
	    /// <returns>Returns an instance of <see cref="IHttpClientBuilder"/></returns>
        public static IHttpClientBuilder AddConfiguredGrpcClient<TService>(
            this IServiceCollection services,
            string url) where TService : ClientBase<TService>
        {
            return services.AddGrpcClient<TService>(opts =>
                {
                    opts.Address = new Uri(url);
                })
                .ConfigureChannel(cfg =>
                {
	                // TODO: separate for prod and dev
                    cfg.HttpHandler = new HttpClientHandler
                    {
                        ServerCertificateCustomValidationCallback =
                            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                    };

                    // TODO: check, why this isn't working in dev
                    // Http configuration
                    // cfg.Credentials = ChannelCredentials.Insecure;

                    // Https configuration
                    // // add SSL credentials
                    // cfg.Credentials = new SslCredentials();
                    // // allow invalid/untrusted certificates
                    // var httpClientHandler = new HttpClientHandler
                    // {
                    //  ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                    // };
                    // var httpClient = new HttpClient(httpClientHandler);
                    // cfg.HttpClient = httpClient;
                })
                .AddInterceptor<AuthHeadersInterceptor>();
        }

	    // TODO: Create implementation.
        public static HttpStatusCode ToHttpStatusCode(this StatusCode statusCode)
        {
            return statusCode switch
            {
                StatusCode.InvalidArgument => HttpStatusCode.BadRequest,
                StatusCode.NotFound => HttpStatusCode.NotFound,
                StatusCode.Unauthenticated => HttpStatusCode.Unauthorized,
                StatusCode.Internal => HttpStatusCode.InternalServerError,
                _ => HttpStatusCode.OK
            };
        }

        /// <summary>
        /// Handles Rpc exceptions.
        /// </summary>
        /// <param name="call">Instance of <see cref="AsyncUnaryCall{TResponse}"/></param>
        /// <typeparam name="TItem">Type of returned data.</typeparam>
        /// <returns>Returns an instance of <see cref="Response"/></returns>
        public static async Task<Response<TItem>> HandleRpcCall<TItem>(
            this AsyncUnaryCall<TItem> call) where TItem : class
        {
	        try
	        {
		        var response = await call;
		        return new Response<TItem> { Data = response };
	        }
	        catch (RpcException e)
	        {
		        var error = JsonSerializer.Deserialize<ErrorResponse<TItem>>(e.Status.Detail);
		        if (error != null && Enum.IsDefined(e.StatusCode))
					error.HttpStatusCode = e.StatusCode.ToHttpStatusCode();
		        return error;
	        }
        }
    }
}