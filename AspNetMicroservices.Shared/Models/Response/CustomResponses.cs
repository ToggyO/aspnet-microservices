using System.Collections.Generic;
using System.Net;

using AspNetMicroservices.Shared.Constants.Errors;

namespace AspNetMicroservices.Shared.Models.Response
{
	public class BadParametersErrorResponse<T> : ErrorResponse<T> where T : class
	{
		public BadParametersErrorResponse(IEnumerable<Error> errors)
		{
			Code = ErrorCodes.Global.BadParameters;
			Message = ErrorMessages.Global.BadParameters;
			HttpStatusCode = HttpStatusCode.BadRequest;

			Errors = errors;
		}

		public BadParametersErrorResponse(Error error) : this(new[] {error})
		{
		}

		public BadParametersErrorResponse() : this(new List<Error>())
		{
		}
	}

	public class BadParametersErrorResponse : ErrorResponse
	{
		public BadParametersErrorResponse(IEnumerable<Error> errors)
		{
			Code = ErrorCodes.Global.BadParameters;
			Message = ErrorMessages.Global.BadParameters;
			HttpStatusCode = HttpStatusCode.BadRequest;

			Errors = errors;
		}

		public BadParametersErrorResponse(Error error) : this(new[] {error})
		{
		}

		public BadParametersErrorResponse() : this(new List<Error>())
		{
		}
	}

	public class NotFoundErrorResponse : ErrorResponse
	{
		public NotFoundErrorResponse(IEnumerable<Error> errors)
		{
			Code = ErrorCodes.Global.NotFound;
			Message = ErrorMessages.Global.NotFound;
			HttpStatusCode = HttpStatusCode.NotFound;

			Errors = errors;
		}

		public NotFoundErrorResponse(Error error) : this(new[] {error})
		{
		}

		public NotFoundErrorResponse() : this(new List<Error>())
		{
		}
	}


	public class NotFoundErrorResponse<T> : ErrorResponse<T> where T : class
	{
		public NotFoundErrorResponse(IEnumerable<Error> errors)
		{
			Code = ErrorCodes.Global.NotFound;
			Message = ErrorMessages.Global.NotFound;
			HttpStatusCode = HttpStatusCode.NotFound;

			Errors = errors;
		}

		public NotFoundErrorResponse(Error error) : this(new[] {error})
		{
		}

		public NotFoundErrorResponse() : this(new List<Error>())
		{
		}
	}

    /// <summary>
    /// Represents the specified internal system http error response data transfer object,
    /// inherited from <see cref="Response"/>.
    /// </summary>
    public class InternalErrorResponse : ErrorResponse
    {
        public InternalErrorResponse(IEnumerable<Error> errors)
        {
            Code = ErrorCodes.System.InternalError;
            Message = ErrorMessages.System.InternalError;
            HttpStatusCode = HttpStatusCode.InternalServerError;
            Errors = errors;
        }

        public InternalErrorResponse(Error error) : this(new[] {error})
        {
        }

        public InternalErrorResponse() : this(new Error[] {})
        {
        }
    }
}