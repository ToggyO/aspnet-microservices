using AspNetMicroservices.Auth.Application.Dto.Users;
using AspNetMicroservices.Shared.Constants.Errors;

using FluentValidation;

namespace AspNetMicroservices.Auth.Application.Features.Users.Validators
{
	public class CreateUpdateUserValidator<TModel> : AbstractValidator<TModel>
		where TModel : CreateUpdateUserDto
	{
		public CreateUpdateUserValidator()
		{
			RuleFor(x => x.FirstName)
				.NotEmpty().NotNull().WithErrorCode(ErrorCodes.Validation.FieldNotEmpty);

			RuleFor(x => x.LastName)
				.NotEmpty().NotNull().WithErrorCode(ErrorCodes.Validation.FieldNotEmpty);

			RuleFor(x => x.Email)
				.EmailAddress().WithErrorCode(ErrorCodes.Validation.FieldEmail);

			RuleFor(x => x.Password)
				.NotNull().NotEmpty().WithErrorCode(ErrorCodes.Validation.FieldNotEmpty);

			// TODO: add pattern validation
			RuleFor(x => x.PhoneNumber)
				.NotEmpty().NotNull().WithErrorCode(ErrorCodes.Validation.FieldNotEmpty);
		}
	}
}