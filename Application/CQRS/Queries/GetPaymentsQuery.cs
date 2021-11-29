using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PaymentsAPI.Application.CQRS.Queries
{
    public class GetPaymentsQuery : IRequest<ObjectResult>
    {
        //In this case GetPaymentsQuery is empty.
        //But in real life can contain some filters or paggination.
        public GetPaymentsQuery()
        {
            
        }
    }
}