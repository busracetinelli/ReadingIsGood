
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.Currencies.ValidationRules;


namespace Business.Handlers.Currencies.Commands
{


	public class UpdateCurrencyCommand : IRequest<IResult>
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Symbol { get; set; }
		public string Code { get; set; }
		public bool IsActive { get; set; }
		public System.DateTime CreatedDate { get; set; }
		public System.DateTime UpdatedDate { get; set; }
		public bool IsDeleted { get; set; }

		public class UpdateCurrencyCommandHandler : IRequestHandler<UpdateCurrencyCommand, IResult>
		{
			private readonly ICurrencyRepository _currencyRepository;
			private readonly IMediator _mediator;

			public UpdateCurrencyCommandHandler(ICurrencyRepository currencyRepository, IMediator mediator)
			{
				_currencyRepository = currencyRepository;
				_mediator = mediator;
			}

			[ValidationAspect(typeof(UpdateCurrencyValidator), Priority = 1)]
			[CacheRemoveAspect("Get")]
			[LogAspect(typeof(FileLogger))]
			[SecuredOperation(Priority = 1)]
			public async Task<IResult> Handle(UpdateCurrencyCommand request, CancellationToken cancellationToken)
			{
				var isThereCurrencyRecord = await _currencyRepository.GetAsync(u => u.Id == request.Id);


				isThereCurrencyRecord.Name = request.Name;
				isThereCurrencyRecord.Symbol = request.Symbol;
				isThereCurrencyRecord.Code = request.Code;
				isThereCurrencyRecord.IsActive = request.IsActive;
				isThereCurrencyRecord.CreatedDate = request.CreatedDate;
				isThereCurrencyRecord.UpdatedDate = request.UpdatedDate;
				isThereCurrencyRecord.IsDeleted = request.IsDeleted;


				_currencyRepository.Update(isThereCurrencyRecord);
				await _currencyRepository.SaveChangesAsync();
				return new SuccessResult(Messages.Updated);
			}
		}
	}
}

