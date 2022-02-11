using AspNetMicroservices.Auth.Application.Dto.Auth;
using AspNetMicroservices.Common.Constants.Errors;

using FluentValidation;

namespace AspNetMicroservices.Auth.Application.Features.Auth.Validators
{
	/// <summary>
	/// Authentication credentials validator.
	/// </summary>
	public class AuthenticateValidator<TModel> : AbstractValidator<TModel>
		where TModel : CredentialsDto
	{
		public AuthenticateValidator()
		{
			RuleFor(x => x.Email)
				.EmailAddress().WithErrorCode(ErrorCodes.Validation.FieldEmail);

			RuleFor(x => x.Password).NotNull().NotEmpty()
				.WithErrorCode(ErrorCodes.Validation.FieldNotEmpty);
		}
	}
}