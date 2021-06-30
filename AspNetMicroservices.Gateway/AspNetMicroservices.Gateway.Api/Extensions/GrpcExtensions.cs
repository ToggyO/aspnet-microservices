using System;
using System.Net;
using System.Net.Http;

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
    }
}