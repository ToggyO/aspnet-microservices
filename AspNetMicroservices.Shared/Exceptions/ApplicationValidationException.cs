using System;
using System.Runtime.Serialization;

using AspNetMicroservices.Shared.Models.Response;

namespace AspNetMicroservices.Shared.Exceptions
{
    // TODO: add description
    [Serializable]
    public class ApplicationValidationException<TItem> : Exception where TItem : class
    {
        private ErrorResponse<TItem> _error;

        public ApplicationValidationException()
        {}

        public ApplicationValidationException(string message) : base(message)
        {}

        public ApplicationValidationException(string message, Exception innerException)
            : base(message, innerException)
        {}

        public ApplicationValidationException(ErrorResponse<TItem> errorResponse, string message = "")
            : this(message)
        {
            _error = errorResponse;
        }

        protected ApplicationValidationException(SerializationInfo info, StreamingContext ctxt) 
            : base(info, ctxt)
        {}

        public ErrorResponse<TItem> GetErrorContent() => _error;
    }

    // TODO: add description
    [Serializable]
    public class ApplicationValidationException : Exception
    {
        private ErrorResponse _error;

        public ApplicationValidationException()
        {}

        public ApplicationValidationException(string message) : base(message)
        {}

        public ApplicationValidationException(string message, Exception innerException)
            : base(message, innerException)
        {}

        public ApplicationValidationException(ErrorResponse errorResponse, string message = "")
            : this(message)
        {
            _error = errorResponse;
        }

        protected ApplicationValidationException(SerializationInfo info, StreamingContext ctxt) 
            : base(info, ctxt)
        {}

        public ErrorResponse GetErrorContent() => _error;
    }
}