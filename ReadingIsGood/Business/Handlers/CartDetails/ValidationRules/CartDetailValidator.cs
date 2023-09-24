
using Business.Handlers.CartDetails.Commands;
using FluentValidation;

namespace Business.Handlers.CartDetails.ValidationRules
{

	public class CreateCartDetailValidator : AbstractValidator<CreateCartDetailCommand>
	{
		public CreateCartDetailValidator()
		{
			RuleFor(x => x.CartId).NotEmpty();
			RuleFor(x => x.ProductId).NotEmpty();
			RuleFor(x => x.Quantity).NotEmpty();
			RuleFor(x => x.CreatedDate).NotEmpty();
			RuleFor(x => x.UpdatedDate).NotEmpty();

		}
	}
	public class UpdateCartDetailValidator : AbstractValidator<UpdateCartDetailCommand>
	{
		public UpdateCartDetailValidator()
		{
			RuleFor(x => x.CartId).NotEmpty();
			RuleFor(x => x.ProductId).NotEmpty();
			RuleFor(x => x.Quantity).NotEmpty();
			RuleFor(x => x.CreatedDate).NotEmpty();
			RuleFor(x => x.UpdatedDate).NotEmpty();

		}
	}
}