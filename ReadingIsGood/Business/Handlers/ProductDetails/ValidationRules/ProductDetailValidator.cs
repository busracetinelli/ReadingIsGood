
using Business.Handlers.ProductDetails.Commands;
using FluentValidation;

namespace Business.Handlers.ProductDetails.ValidationRules
{

	public class CreateProductDetailValidator : AbstractValidator<CreateProductDetailCommand>
	{
		public CreateProductDetailValidator()
		{
			RuleFor(x => x.ProductId).NotEmpty();
			RuleFor(x => x.ProductName).NotEmpty();
			RuleFor(x => x.ProductDescription).NotEmpty();
			RuleFor(x => x.ProductStock).NotEmpty();
			RuleFor(x => x.ProductPrice).NotEmpty();

		}
	}
	public class UpdateProductDetailValidator : AbstractValidator<UpdateProductDetailCommand>
	{
		public UpdateProductDetailValidator()
		{
			RuleFor(x => x.ProductId).NotEmpty();
			RuleFor(x => x.ProductName).NotEmpty();
			RuleFor(x => x.ProductDescription).NotEmpty();
			RuleFor(x => x.ProductStock).NotEmpty();
			RuleFor(x => x.ProductPrice).NotEmpty();

		}
	}
}