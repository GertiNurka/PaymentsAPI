using PaymentsDomain.SeedWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaymentsDomain.AggregatesModel.PaymentAggregate
{
    public interface IPaymentsRepository : IRepository<Payment>
    {
        Task<IEnumerable<Payment>> FindAll();
        Task<Payment> FindById(int id);
        Task Update(Payment payment);
    }
}
