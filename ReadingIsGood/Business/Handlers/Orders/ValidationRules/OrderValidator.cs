
using Business.Handlers.Orders.Commands;
using FluentValidation;

namespace Business.Handlers.Orders.ValidationRules
{

	public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
	{
		public CreateOrderValidator()
		{
			RuleFor(x => x.UserId).NotEmpty();
			RuleFor(x => x.CreatedDate).NotEmpty();
			RuleFor(x => x.UpdatedDate).NotEmpty();
			RuleFor(x => x.DeletedDate).NotEmpty();
			RuleFor(x => x.IsActive).NotEmpty();
			RuleFor(x => x.IsDeleted).NotEmpty();

		}
	}
	public class UpdateOrderValidator : AbstractValidator<UpdateOrderCommand>
	{
		public UpdateOrderValidator()
		{
			RuleFor(x => x.UserId).NotEmpty();
			RuleFor(x => x.CreatedDate).NotEmpty();
			RuleFor(x => x.UpdatedDate).NotEmpty();
			RuleFor(x => x.DeletedDate).NotEmpty();
			RuleFor(x => x.IsActive).NotEmpty();
			RuleFor(x => x.IsDeleted).NotEmpty();

		}
	}
}