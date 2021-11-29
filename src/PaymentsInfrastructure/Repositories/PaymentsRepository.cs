using Microsoft.EntityFrameworkCore;
using PaymentsDomain.AggregatesModel.PaymentAggregate;
using PaymentsDomain.AggregatesModel.Validators;
using PaymentsDomain.SeedWork;
using PaymentsInfrastructure.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaymentsInfrastructure.Repositories
{
    public class PaymentsRepository : BaseRepository<Payment>, IPaymentsRepository
    {
        private readonly PaymentsDbContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public PaymentsRepository(PaymentsDbContext context) : base(new PaymentValidator())
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Payment>> FindAll()
        {
            var payments = await _context.Payments.ToListAsync();

            return payments;
        }

        public async Task<Payment> FindById(int id)
        {
            var payment = await _context.Payments.SingleOrDefaultAsync(x => x.Id == id);

            return payment;
        }

        public async Task Update(Payment payment)
        {
            await ValidateEntity(payment);

            await Task.Run(() => _context.Payments.Update(payment));
        }


    }
}
