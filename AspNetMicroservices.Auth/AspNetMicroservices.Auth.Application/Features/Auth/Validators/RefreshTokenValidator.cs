﻿using AspNetMicroservices.Auth.Application.Dto.Auth;
using AspNetMicroservices.Shared.Constants.Errors;

using FluentValidation;

namespace AspNetMicroservices.Auth.Application.Features.Auth.Validators
{
	/// <summary>
	/// Refresh token validator.
	/// </summary>
	public class RefreshTokenValidator : AbstractValidator<RefreshTokenDto>
	{
		public RefreshTokenValidator()
		{
			RuleFor(x => x.RefreshToken)
				.NotEmpty().NotNull().WithErrorCode(ErrorCodes.Validation.FieldNotEmpty);
		}
	}
}