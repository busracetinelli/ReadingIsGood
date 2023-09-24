
using Business.Handlers.Products.Commands;
using FluentValidation;

namespace Business.Handlers.Products.ValidationRules
{

	public class CreateProductValidator : AbstractValidator<CreateProductCommand>
	{
		public CreateProductValidator()
		{
			RuleFor(x => x.Code).NotEmpty();
			RuleFor(x => x.CreatedDate).NotEmpty();
			RuleFor(x => x.UpdatedDate).NotEmpty();
			RuleFor(x => x.IsDeleted).NotEmpty();
			RuleFor(x => x.IsActive).NotEmpty();

		}
	}
	public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
	{
		public UpdateProductValidator()
		{
			RuleFor(x => x.Code).NotEmpty();
			RuleFor(x => x.CreatedDate).NotEmpty();
			RuleFor(x => x.UpdatedDate).NotEmpty();
			RuleFor(x => x.IsDeleted).NotEmpty();
			RuleFor(x => x.IsActive).NotEmpty();

		}
	}
}