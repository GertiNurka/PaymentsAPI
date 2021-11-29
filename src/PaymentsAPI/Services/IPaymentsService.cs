using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PaymentsAPI.Application.Models;
using PaymentsDomain.AggregatesModel.PaymentAggregate;

namespace PaymentsAPI.Services
{
    public interface IPaymentsService
    {
        Task<ServiceResponse<IEnumerable<Payment>>> Get();
        Task<ServiceResponse<Payment>> Get(int id);
        Task<ServiceResponse<bool?>> SavePayment(Payment payment);
        Task<ServiceResponse<bool?>> Save(CancellationToken cancellationToken);
    }
}