using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using AspNetMicroservices.Abstractions.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AspNetMicroservices.Auth.Api.Middleware
{
    /// <summary>
    /// Middleware handles unhandled exceptions.
    /// </summary>
    public class ExceptionMiddleware
    {
        /// <summary>
        /// Initialize new instance of <see cref="ExceptionMiddleware"/>
        /// </summary>
        /// <param name="next">Instance of <see cref="RequestDelegate"/></param>
        /// <param name="logger">Instance of <see cref="ILogger{TCategoryName}"/></param>
        public ExceptionMiddleware(RequestDelegate next,
            ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Instance of <see cref="RequestDelegate"/>
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// Instance of <see cref="ILogger{TCategoryName}"/>
        /// </summary>
        private readonly ILogger<ExceptionMiddleware> _logger;

        /// <summary>
        /// Method executes next middleware or handle unhandled exceptions.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/> for the request.</param>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                await HandleExceptionAsync(context);
            }
        }

        /// <summary>
        /// Method handle unhandled http exceptions.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/> for the request.</param>
        /// <returns><see cref="Task"/></returns>
        private Task HandleExceptionAsync(HttpContext context)
        {
            var error = new InternalErrorResponse();

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            var response = JsonSerializer.Serialize(error, jsonSerializerOptions);
            return context.Response.WriteAsync(response);
        }
    }
}