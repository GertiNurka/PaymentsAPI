using FluentValidation;
using PaymentsDomain.AggregatesModel.PaymentAggregate;

namespace PaymentsDomain.AggregatesModel.Validators
{
    public class CardValidator : AbstractValidator<Card>
    {
        public CardValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .MaximumLength(10);
            RuleFor(x => x.CardNumber)
                .NotEmpty()
                .NotNull()
                .MaximumLength(16)
                .MinimumLength(16);
            RuleFor(x => x.ExpiryDate)
                .NotEmpty()
                .NotNull()
                .MaximumLength(4)
                .MinimumLength(4);
            RuleFor(x => x.Cvv)
                .NotEmpty()
                .NotNull()
                .MaximumLength(3)
                .MinimumLength(3);

        }
    }
}
