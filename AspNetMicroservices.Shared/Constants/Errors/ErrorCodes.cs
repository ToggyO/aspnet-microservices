namespace AspNetMicroservices.Shared.Constants.Errors
{
    public static class ErrorCodes
    {
        public static class Global
        {
            public const string BusinessConflict = "business_conflict";
            public const string UnprocessableEntity = "unprocessable_entity";
            public const string BadParameters = "bad_parameters";
            public const string NotFound = "not_found";
            public const string PermissionDenied = "permission_denied";
            public const string Unauthorized = "security_error";
            public const string Forbidden = "forbidden";
        }

        public static class Validation
        {
            public const string FieldInvalidType = "validation.field_invalid_type";
            public const string FieldNotEmpty = "validation.field_not_empty";
            public const string FieldSizeMax = "validation.field_size_max";
            public const string FieldSizeMin = "validation.field_size_min";
            public const string FieldInvalidLength = "validation.field_invalid_length";
            public const string FieldNotValidChars = "validation.field_not_valid_chars";
            public const string FieldMax = "validation.field_max";
            public const string FieldMin = "validation.field_min";
            public const string FieldFuture = "validation.field_future";
            public const string FieldPast = "validation.field_past";
            public const string FieldEmail = "validation.field_email";
            public const string FieldCardNumber = "validation.field_card_number";
            public const string FieldPhone = "validation.field_phone";
            public const string FieldDuplicate = "validation.field_duplicate";
        }

        public static class Security
        {
            public const string AuthDataInvalid = "sec.auth_data_invalid";
            public const string TokenInvalid = "sec.token_invalid";
            public const string AccessTokenInvalid = "sec.access_token_invalid";
            public const string AccessTokenExpired = "sec.access_token_expired";
            public const string GoogleTokenInvalid = "sec.google_token_invalid";
            public const string RefreshTokenInvalid = "sec.refresh_token_invalid";
            public const string RefreshTokenExpired = "sec.refresh_token_expired";
        }

        public static class Business
        {
            public const string EmailExists = "bus.email_already_exists";
            public const string InvalidEmail = "bus.invalid_email";
            public const string UserDoesNotExists = "bus.user_does_not_exists";
            public const string UserIsDeleted = "bus.user_is_deleted";
            public const string UserIsBlocked = "bus.user_is_blocked";
            public const string InvalidRegistrationStep = "bus.invalid_registration_step";
            public const string PasswordChangeRequestInvalid = "bus.password_change_request_invalid";
        }

        public static class System
        {
            public const string InternalError = "internal_server_error";
        }
    }
}