using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentsAPI.Application.CQRS.Commands;
using PaymentsAPI.Services;
using PaymentsDomain.AggregatesModel.PaymentAggregate;

namespace PaymentsAPI.Application.CQRS.Handlers.CommandHandlers
{
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, ObjectResult>
    {
        private readonly ILogger<CreatePaymentCommandHandler> _logger;
        private readonly IPaymentsService _paymentsService;
        private readonly IMapper _mapper;

        public CreatePaymentCommandHandler(ILogger<CreatePaymentCommandHandler> logger, IPaymentsService paymentsService, IMapper mapper)
        {
            _logger = logger;
            _paymentsService = paymentsService;
            _mapper = mapper;
        }

        public async Task<ObjectResult> Handle(CreatePaymentCommand command, CancellationToken cancellationToken)
        {
            var card = _mapper.Map<Card>(command.Card);
            var billingAddress = _mapper.Map<BillingAddress>(command.BillingAddress);

            var payment = new Payment(command.Amount, card, billingAddress);
            
            var serviceResponse = await _paymentsService.SavePayment(payment);
            if (!serviceResponse.Success)
            {
                _logger.LogInformation($"Calling {nameof(IPaymentsService)} to create payment failed with status code {serviceResponse.StatusCode}.");
                return serviceResponse.GetObjectResult();
            }

            var saved = await _paymentsService.Save(cancellationToken);
            if (!saved.Success)
            {
                _logger.LogInformation($"Calling {typeof(IPaymentsService).Name} to save payment failed.");
                return saved.GetObjectResult();
            }

            return new CreatedAtRouteResult("GetById", new { id = payment.Id }, payment.Id);
        }
    }
}
