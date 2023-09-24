
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.Currencies.ValidationRules;

namespace Business.Handlers.Currencies.Commands
{
	/// <summary>
	/// 
	/// </summary>
	public class CreateCurrencyCommand : IRequest<IResult>
	{

		public string Name { get; set; }
		public string Symbol { get; set; }
		public string Code { get; set; }
		public bool IsActive { get; set; }
		public System.DateTime CreatedDate { get; set; }
		public System.DateTime UpdatedDate { get; set; }
		public bool IsDeleted { get; set; }


		public class CreateCurrencyCommandHandler : IRequestHandler<CreateCurrencyCommand, IResult>
		{
			private readonly ICurrencyRepository _currencyRepository;
			private readonly IMediator _mediator;
			public CreateCurrencyCommandHandler(ICurrencyRepository currencyRepository, IMediator mediator)
			{
				_currencyRepository = currencyRepository;
				_mediator = mediator;
			}

			[ValidationAspect(typeof(CreateCurrencyValidator), Priority = 1)]
			[CacheRemoveAspect("Get")]
			[LogAspect(typeof(FileLogger))]
			[SecuredOperation(Priority = 1)]
			public async Task<IResult> Handle(CreateCurrencyCommand request, CancellationToken cancellationToken)
			{
				var isThereCurrencyRecord = _currencyRepository.Query().Any(u => u.Name == request.Name);

				if (isThereCurrencyRecord == true)
					return new ErrorResult(Messages.NameAlreadyExist);

				var addedCurrency = new Currency
				{
					Name = request.Name,
					Symbol = request.Symbol,
					Code = request.Code,
					IsActive = request.IsActive,
					CreatedDate = request.CreatedDate,
					UpdatedDate = request.UpdatedDate,
					IsDeleted = request.IsDeleted,

				};

				_currencyRepository.Add(addedCurrency);
				await _currencyRepository.SaveChangesAsync();
				return new SuccessResult(Messages.Added);
			}
		}
	}
}