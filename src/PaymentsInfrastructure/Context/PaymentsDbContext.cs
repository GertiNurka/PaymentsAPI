using Microsoft.EntityFrameworkCore;
using PaymentsDomain.SeedWork;
using PaymentsInfrastructure.EntityConfigurations;
using System.Threading;
using System.Threading.Tasks;
using PaymentsDomain.AggregatesModel.PaymentAggregate;

namespace PaymentsInfrastructure.Context
{
    public class PaymentsDbContext : DbContext, IUnitOfWork
    {
        public PaymentsDbContext(DbContextOptions<PaymentsDbContext> options) : base(options) { }

        public virtual DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new PaymentConfiguration());

            base.OnModelCreating(builder);
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await base.SaveChangesAsync(cancellationToken);

            return true;
        }

        
    }
}
