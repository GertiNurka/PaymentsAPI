using System.Collections.Generic;
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
    public class GetPaymentsQueryHandler : IRequestHandler<GetPaymentsQuery, ObjectResult>
    {
        private readonly ILogger<GetPaymentsQueryHandler> _logger;
        private readonly IPaymentsService _paymentsService;
        private readonly IMapper _mapper;

        public GetPaymentsQueryHandler(ILogger<GetPaymentsQueryHandler> logger, IPaymentsService paymentsService, IMapper mapper)
        {
            _logger = logger;
            _paymentsService = paymentsService;
            _mapper = mapper;
        }

        public async Task<ObjectResult> Handle(GetPaymentsQuery query, CancellationToken cancellationToken)
        {
            var serviceResponse = await _paymentsService.Get();
            if (!serviceResponse.Success)
            {
                _logger.LogInformation($"Calling {nameof(IPaymentsService)} to get payments failed with status code {serviceResponse.StatusCode}.");
                return serviceResponse.GetObjectResult();
            }

            var payment = serviceResponse.Data;

            var paymentDto = _mapper.Map<IEnumerable<PaymentDto>>(payment);

            return new OkObjectResult(paymentDto);
        }
    }
}