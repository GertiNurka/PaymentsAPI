using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentsAPI.Application.CQRS.Queries;
using PaymentsAPI.Application.Dtos;
using PaymentsAPI.Services;

namespace PaymentsAPI.Application.CQRS.Handlers.QueryHandlers
{
    public class GetPaymentQueryHandler : IRequestHandler<GetPaymentQuery, ObjectResult>
    {
        private readonly ILogger<GetPaymentQueryHandler> _logger;
        private readonly IPaymentsService _paymentsService;
        private readonly IMapper _mapper;

        public GetPaymentQueryHandler(ILogger<GetPaymentQueryHandler> logger, IPaymentsService paymentsService, IMapper mapper)
        {
            _logger = logger;
            _paymentsService = paymentsService;
            _mapper = mapper;
        }

        public async Task<ObjectResult> Handle(GetPaymentQuery query, CancellationToken cancellationToken)
        {
            var serviceResponse = await _paymentsService.Get(query.Id);
            if (!serviceResponse.Success)
            {
                _logger.LogInformation($"Calling {nameof(IPaymentsService)} to get payment failed with status code {serviceResponse.StatusCode}.");
                return serviceResponse.GetObjectResult();
            }

            var payment = serviceResponse.Data;

            var paymentDto = _mapper.Map<PaymentDto>(payment);

            return new OkObjectResult(paymentDto);
        }
    }
}
