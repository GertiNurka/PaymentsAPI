using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaymentsAPI.Application.Dtos;

namespace PaymentsAPI.Application.CQRS.Commands
{
    public class CreatePaymentCommand : IRequest<ObjectResult>
    {
        public decimal Amount { get; set; }
        public string Name { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string Cvv { get; set; }

        public BillingAddressDto BillingAddress { get; set; }

        public CreatePaymentCommand() { }

        public CreatePaymentCommand(decimal amount, string name, string cardNumber, string expiryDate, string cvv,  BillingAddressDto billingAddress) : this()
        {
            Amount = amount;
            Name = name;
            CardNumber = cardNumber;
            ExpiryDate = expiryDate;
            Cvv = cvv;
            BillingAddress = billingAddress;
        }

        public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
        {
            public CreatePaymentCommandValidator()
            {
                RuleFor(x => x.Amount)
                    .GreaterThan(0);
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
                RuleFor(x => x.BillingAddress)
                    .NotNull()
                    .SetValidator(new BillingAddressDtoValidator());
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
