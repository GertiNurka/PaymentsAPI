using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PaymentsAPI.Application.Models;
using PaymentsDomain.AggregatesModel.PaymentAggregate;

namespace PaymentsAPI.Services
{
    public class PaymentsService : IPaymentsService
    {
        private readonly ILogger<PaymentsService> _logger;
        private readonly IPaymentsRepository _paymentsRepository;

        public PaymentsService(ILogger<PaymentsService> logger, IPaymentsRepository paymentsRepository)
        {
            _logger = logger;
            _paymentsRepository = paymentsRepository;
        }

        /// <summary>
        /// Get payment
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResponse<IEnumerable<Payment>>> Get()
        {
            var payments = await _paymentsRepository.FindAll();
            if (!payments.Any())
            {
                var msg = $"No {nameof(Payment)}s found.";
                _logger.LogInformation(msg);
                return new ServiceResponse<IEnumerable<Payment>>(true, StatusCodes.Status204NoContent, msg);
            }

            return new ServiceResponse<IEnumerable<Payment>>(true, StatusCodes.Status200OK, null, payments);
        }

        /// <summary>
        /// Get payment 
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResponse<Payment>> Get(int id)
        {
            var payment = await _paymentsRepository.FindById(id);
            if (payment == null)
            {
                var msg = $"{nameof(Payment)} for Id: {id} couldn't be found.";
                _logger.LogInformation(msg);
                return new ServiceResponse<Payment>(false, StatusCodes.Status404NotFound, msg);
            }

            return new ServiceResponse<Payment>(true, StatusCodes.Status200OK, null, payment);
        }

        /// <summary>
        /// Save payment
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<bool?>> SavePayment(Payment payment)
        {
            await _paymentsRepository.Update(payment);

            return new ServiceResponse<bool?>(true, StatusCodes.Status200OK, null, null);
        }

        /// <summary>
        /// Save changes
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<bool?>> Save(CancellationToken cancellationToken)
        {
            var saved = await _paymentsRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            if (!saved)
            {
                var msg = $"Saving {nameof(Payment)} failed.";
                _logger.LogInformation(msg);
                return new ServiceResponse<bool?>(false, StatusCodes.Status500InternalServerError, msg);
            }

            return new ServiceResponse<bool?>(true, StatusCodes.Status200OK, null, null);
        }
    }
}