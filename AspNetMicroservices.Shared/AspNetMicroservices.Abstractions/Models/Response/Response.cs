using System.Collections.Generic;
using System.Net;
using System.Text.Json.Serialization;

namespace AspNetMicroservices.Abstractions.Models.Response
{
    /// <summary>
    /// Represents the basic application http response data transfer object.
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Http status code <see cref="https://en.wikipedia.org/wiki/List_of_HTTP_status_codes"/>.
        /// </summary>
        [JsonIgnore]
        public HttpStatusCode HttpStatusCode { get; set; } = HttpStatusCode.OK;

        /// <summary>
        /// Gives information about the success of the operation.
        /// </summary>
        [JsonIgnore]
        public bool IsSuccess
        {
	        get
	        {
		        var asInt = (int)HttpStatusCode;
		        return asInt is >= 200 and <= 299;
	        }
        }

        /// <summary>
        /// Gets or sets a value that determines what type of response are returned from the application.
        /// </summary>
        public dynamic Code { get; set; } = "success";
    }

    /// <summary>
    /// Represents the http response data transfer object extended by typed data.
    /// </summary>
    /// <typeparam name="T">A type that inherits from the <see cref="Response"/> class.</typeparam>
    public class Response<T> : Response where T : class
    {
        /// <summary>
        /// Gets or sets a value that determines typed data that returns from application.
        /// </summary>
        public T Data { get; set; }
    }

    /// <summary>
    /// Represents the http error response data transfer object,
    /// inherited from <see cref="Response"/>.
    /// </summary>
    public class ErrorResponse : Response
    {
        /// <summary>
        /// Gets or sets a value determines error response message returned from application.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets of sets a collection of <see cref="Error"/>, represents validation errors.
        /// </summary>
        public IEnumerable<Error> Errors { get; set; } = new Error[] {};
    }

    /// <summary>
    /// Represents the http error response typed data transfer object, inherited from <see cref="Response"/>.
    /// </summary>
    /// <typeparam name="T">A type that inherits from the <see cref="Response"/> class.</typeparam>
    public class ErrorResponse<T> : Response<T> where T : class
    {
        /// <summary>
        /// Gets or sets a value determines error response message returned from application.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets of sets a collection of <see cref="Error"/>, represents validation errors.
        /// </summary>
        public IEnumerable<Error> Errors { get; set; } = new Error[] {};
    }

    /// <summary>
    /// Represents the validation error info.
    /// </summary>
    public class Error
    {
        /// <summary>
        /// Gets or sets a value indicating the type of validation error.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets a value represents the message of validation error.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets a value represents the name of the invalid parameter.
        /// </summary>
        public string Field { get; set; }
    }
}