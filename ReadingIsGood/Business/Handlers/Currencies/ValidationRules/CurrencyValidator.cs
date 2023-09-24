
using Business.Handlers.Currencies.Commands;
using FluentValidation;

namespace Business.Handlers.Currencies.ValidationRules
{

	public class CreateCurrencyValidator : AbstractValidator<CreateCurrencyCommand>
	{
		public CreateCurrencyValidator()
		{
			RuleFor(x => x.Name).NotEmpty();
			RuleFor(x => x.Symbol).NotEmpty();
			RuleFor(x => x.Code).NotEmpty();
			RuleFor(x => x.IsActive).NotEmpty();
			RuleFor(x => x.CreatedDate).NotEmpty();
			RuleFor(x => x.UpdatedDate).NotEmpty();
			RuleFor(x => x.IsDeleted).NotEmpty();

		}
	}
	public class UpdateCurrencyValidator : AbstractValidator<UpdateCurrencyCommand>
	{
		public UpdateCurrencyValidator()
		{
			RuleFor(x => x.Name).NotEmpty();
			RuleFor(x => x.Symbol).NotEmpty();
			RuleFor(x => x.Code).NotEmpty();
			RuleFor(x => x.IsActive).NotEmpty();
			RuleFor(x => x.CreatedDate).NotEmpty();
			RuleFor(x => x.UpdatedDate).NotEmpty();
			RuleFor(x => x.IsDeleted).NotEmpty();

		}
	}
}