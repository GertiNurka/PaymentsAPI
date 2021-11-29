using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaymentsAPI.Application.Dtos;

namespace PaymentsAPI.Application.CQRS.Commands
{
    public class CreatePaymentCommand : IRequest<ObjectResult>
    {
        public decimal Amount { get; set; }

        public CardDto Card { get; set; }

        public BillingAddressDto BillingAddress { get; set; }

        public CreatePaymentCommand() { }

        public CreatePaymentCommand(decimal amount, CardDto card, BillingAddressDto billingAddress) : this()
        {
            Amount = amount;
            Card = card;
            BillingAddress = billingAddress;
        }

        public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
        {
            public CreatePaymentCommandValidator()
            {
                RuleFor(x => x.Amount)
                    .GreaterThan(0);
                RuleFor(x => x.Card)
                    .NotNull()
                    .SetValidator(new CardDtoValidator());
                RuleFor(x => x.BillingAddress)
                    .NotNull()
                    .SetValidator(new BillingAddressDtoValidator());
            }
        }

        public class CardDtoValidator : AbstractValidator<CardDto>
        {
            public CardDtoValidator()
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

        public class BillingAddressDtoValidator : AbstractValidator<BillingAddressDto>
        {
            public BillingAddressDtoValidator()
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
}
