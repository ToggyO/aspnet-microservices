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

        public static HttpStatusCode ToHttpStatusCode(this StatusCode statusCode)
        {
            return statusCode switch
            {
                // Sta
                _ => HttpStatusCode.OK
            };
        }

        public static async Task<Response<TItem>> EnsureSuccess<TItem>(
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
		        return error;
	        }
        }
    }
}