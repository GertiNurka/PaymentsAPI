using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentsAPI.Application.Dtos
{
    public class PaymentDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public CardDto Card { get; set; }
        public BillingAddressDto BillingAddress { get; set; }
    }
}
