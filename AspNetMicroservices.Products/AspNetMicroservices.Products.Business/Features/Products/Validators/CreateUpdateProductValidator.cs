using AspNetMicroservices.Products.Business.Features.Products.Models;
using AspNetMicroservices.Shared.Errors;

using FluentValidation;

namespace AspNetMicroservices.Products.Business.Features.Products.Validators
{
	/// <summary>
	/// Validator for <see cref="CreateUpdateProductValidator{TModel}"/>.
	/// </summary>
	public class CreateUpdateProductValidator<TModel> : AbstractValidator<TModel>
		where TModel : CreateProductModel
	{
		/// <summary>
		/// Creates an instance of <see cref="CreateUpdateProductValidator{TModel}"/>.
		/// </summary>
		public CreateUpdateProductValidator()
		{
			RuleFor(x => x.Name).NotEmpty().WithErrorCode(ErrorCodes.Validation.FieldNotEmpty);
			RuleFor(x => x.Price).NotEmpty().WithErrorCode(ErrorCodes.Validation.FieldNotEmpty);
		}
	}
}