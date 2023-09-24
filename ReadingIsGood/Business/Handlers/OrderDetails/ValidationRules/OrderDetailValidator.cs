
using Business.Handlers.OrderDetails.Commands;
using FluentValidation;

namespace Business.Handlers.OrderDetails.ValidationRules
{

	public class CreateOrderDetailValidator : AbstractValidator<CreateOrderDetailCommand>
	{
		public CreateOrderDetailValidator()
		{
			RuleFor(x => x.OrderId).NotEmpty();
			RuleFor(x => x.ProductName).NotEmpty();
			RuleFor(x => x.Quantity).NotEmpty();
			RuleFor(x => x.Price).NotEmpty();
			RuleFor(x => x.CreatedDate).NotEmpty();
			RuleFor(x => x.UpdatedDate).NotEmpty();
			RuleFor(x => x.IsActive).NotEmpty();
			RuleFor(x => x.IsDeleted).NotEmpty();

		}
	}
	public class UpdateOrderDetailValidator : AbstractValidator<UpdateOrderDetailCommand>
	{
		public UpdateOrderDetailValidator()
		{
			RuleFor(x => x.OrderId).NotEmpty();
			RuleFor(x => x.ProductName).NotEmpty();
			RuleFor(x => x.Quantity).NotEmpty();
			RuleFor(x => x.Price).NotEmpty();
			RuleFor(x => x.CreatedDate).NotEmpty();
			RuleFor(x => x.UpdatedDate).NotEmpty();
			RuleFor(x => x.IsActive).NotEmpty();
			RuleFor(x => x.IsDeleted).NotEmpty();

		}
	}
}