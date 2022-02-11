using AspNetMicroservices.Auth.Application.Dto.Auth;
using AspNetMicroservices.Common.Constants.Errors;

using FluentValidation;

namespace AspNetMicroservices.Auth.Application.Features.Auth.Validators
{
	/// <summary>
	/// Refresh token validator.
	/// </summary>
	public class RefreshTokenValidator<TModel> : AbstractValidator<TModel>
		where TModel : RefreshTokenDto
	{
		public RefreshTokenValidator()
		{
			RuleFor(x => x.RefreshToken)
				.NotEmpty().NotNull().WithErrorCode(ErrorCodes.Validation.FieldNotEmpty);
		}
	}
}