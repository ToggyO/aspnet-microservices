using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AspNetMicroservices.Shared.Models.Response;
using AspNetMicroservices.Shared.Protos.ProductsProtos;
using Grpc.Core;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetMicroservices.Gateway.Api.Extensions
{
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
                return new Response<TItem>
                {
                    Data = response,
                };
            }
            catch (RpcException e)
            {
                return new ErrorResponse<TItem>
                {
                    HttpStatusCode = e.StatusCode.ToHttpStatusCode(),
                    Message = e.Status.Detail,
                };
            }
        }
    }
}