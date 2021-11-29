using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentsDomain.AggregatesModel.PaymentAggregate;

namespace PaymentsInfrastructure.EntityConfigurations
{
    class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Amount).IsRequired();

            builder.OwnsOne(x => x.Card, cardBuilder =>
            {
                cardBuilder.WithOwner();

                cardBuilder.Property(x => x.Name).IsRequired().HasMaxLength(10);
                cardBuilder.Property(x => x.CardNumber).IsRequired().HasMaxLength(16);
                cardBuilder.Property(x => x.ExpiryDate).IsRequired().HasMaxLength(4);
                cardBuilder.Property(x => x.Cvv).IsRequired().HasMaxLength(3);
            });

            builder.OwnsOne(x => x.BillingAddress, billingAddressBuilder =>
            {
                billingAddressBuilder.WithOwner();

                billingAddressBuilder.Property(x => x.Line1).IsRequired().HasMaxLength(20);
                billingAddressBuilder.Property(x => x.Line2).HasMaxLength(20);
                billingAddressBuilder.Property(x => x.Line3).HasMaxLength(20);
                billingAddressBuilder.Property(x => x.PostCode).IsRequired().HasMaxLength(7);
            });
        }
    }
}
