using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using Grpc.Core;
using Microsoft.Extensions.DependencyInjection;

using AspNetMicroservices.Shared.Models.Response;

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
                    cfg.HttpHandler = new HttpClientHandler
                    {
                        ServerCertificateCustomValidationCallback =
                            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                    };
                });
        }

	    // TODO: Create implementation.
        public static HttpStatusCode ToHttpStatusCode(this StatusCode statusCode)
        {
            return statusCode switch
            {
                StatusCode.InvalidArgument => HttpStatusCode.BadRequest,
                StatusCode.NotFound => HttpStatusCode.NotFound,
                _ => HttpStatusCode.OK
            };
        }

        /// <summary>
        /// Handles Rpc exceptions.
        /// </summary>
        /// <param name="call">Instance of <see cref="AsyncUnaryCall{TResponse}"/></param>
        /// <typeparam name="TItem">Type of returned data.</typeparam>
        /// <returns>Returns an instance of <see cref="Response"/></returns>
        public static async Task<Response<TItem>> EnsureRpcCallSuccess<TItem>(
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