using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PaymentsAPI.Application.CQRS.Queries
{
    public class GetPaymentQuery : IRequest<ObjectResult>
    {
        public int Id { get; private set; }

        public GetPaymentQuery(int id)
        {
            Id = id;
        }
    }
}
