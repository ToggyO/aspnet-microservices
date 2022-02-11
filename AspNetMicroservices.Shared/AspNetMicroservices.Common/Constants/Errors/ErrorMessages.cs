using System;

namespace AspNetMicroservices.Common.Constants.Errors
{
    public static class ErrorMessages
    {
        public static class Global
        {
            public const string BusinessConflict = "Business conflict";
            public const string UnprocessableEntity = "Invalid entity or business request";
            public const string BadParameters = "Bad parameters";
            public const string NotFound = "Not found";
            public const string PermissionDenied = "Permission denied";
            public const string Unauthorized = "Unauthorized";
            public const string Forbidden = "Forbidden";
            public const string FieldInvalid = "Invalid field";
        };

        public static class Validation
        {
            public const string FieldNotEmpty = "The field can't be empty";
            public const string FieldSizeMax = "The field is too long";
            public const string FieldSizeMin = "The field is too short";
            public const string FieldInvalidLength = "The field length is not correct";
            public const string FieldNotValidChars = "The field contains invalid characters";
            public const string FieldEmail = "Email isn't valid";
            public const string FieldCardNumber = "Card number isn't valid";
            public const string FieldPhone = "The phone number isn't valid";
            public const string FieldDuplicate = "The field value should be unique";

            public static string FieldMax(int maxNumber) => $"The number can't be greater than ${maxNumber}";
            public static string FieldMin(int minNumber) => "The number can't be less than ${minNumber}";
            public static string FieldFuture(DateTime maxDate) => $"The date should be later than ${maxDate}";
            public static string FieldPast(DateTime minDate) => $"The date should be early than ${minDate}";
        };

        public static class Security
        {
            public const string AuthDataInvalid = "Auth data invalid";
            public const string TokenInvalid = "Token invalid";
            public const string AccessTokenInvalid = "Access token invalid";
            public const string AccessTokenExpired = "Access token expired";
            public const string RefreshTokenInvalid = "Refresh token invalid";
            public const string RefreshTokenExpired = "Refresh token expired";
        }

        public static class Business
        {
            public const string EmailExists = "This email is already registered. Sign in or use different email to register";
            public const string InvalidEmail = "Email is invalid";
            public const string UserDoesNotExists = "UserOrmEntity does not exists";
            public const string UserIsDeleted = "You don't have access to system - your account is deleted";
            public const string UserIsBlocked = "You don't have access to system - your account is blocked";
            public const string PasswordChangeRequestInvalid = "Password change request is invalid";
            public const string PasswordChangeCodeInvalid = "Code for change password invalid";
        };

        public static class System
        {
            public const string InternalError = "Internal server error";
        }
    }
}