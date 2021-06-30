using System.Collections.Generic;
using AspNetMicroservices.Shared.Errors;

namespace AspNetMicroservices.Shared.Models.Response
{
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