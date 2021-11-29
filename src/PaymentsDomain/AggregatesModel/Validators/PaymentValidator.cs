using FluentValidation;
using PaymentsDomain.AggregatesModel.PaymentAggregate;

namespace PaymentsDomain.AggregatesModel.Validators
{
    public class PaymentValidator : AbstractValidator<Payment>
    {
        public PaymentValidator()
        {
            RuleFor(x => x.Amount)
                    .GreaterThan(0);
            RuleFor(x => x.Card)
                .NotNull()
                .SetValidator(new CardValidator());
            RuleFor(x => x.BillingAddress)
                .NotNull()
                .SetValidator(new BillingAddressValidator());
        }
    }
}
