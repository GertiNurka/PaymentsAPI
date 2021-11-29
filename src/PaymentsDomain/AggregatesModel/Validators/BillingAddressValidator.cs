using FluentValidation;
using PaymentsDomain.AggregatesModel.PaymentAggregate;

namespace PaymentsDomain.AggregatesModel.Validators
{
    public class BillingAddressValidator : AbstractValidator<BillingAddress>
    {
        public BillingAddressValidator()
        {
            RuleFor(x => x.Line1)
                .NotEmpty()
                .NotNull()
                .MaximumLength(20);
            RuleFor(x => x.Line2)
                .MaximumLength(20);
            RuleFor(x => x.Line3)
                .MaximumLength(20);
            RuleFor(x => x.PostCode)
                .NotEmpty()
                .NotNull()
                .MaximumLength(7);

        }
    }
}
