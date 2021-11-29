using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using PaymentsAPI.Application.CQRS.Commands;
using PaymentsAPI.Application.CQRS.Queries;

namespace PaymentsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentsController : BaseController<PaymentsController>
    {
        public PaymentsController(ILogger<PaymentsController> logger, IMediator mediator) : base(mediator, logger)
        {
        }

        /// <summary>
        /// Get payments
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get()
        {
            base.Logger.LogInformation($"Sending {nameof(GetPaymentsQuery)}.");

            //Send query by Mediator
            var handlerResponse = await base.Mediator.Send(new GetPaymentsQuery());

            base.Logger.LogInformation($"{nameof(GetPaymentsQuery)} completed with status code {handlerResponse.StatusCode}.");

            return handlerResponse;
        }

        /// <summary>
        /// Get payment by id
        /// </summary>
        /// <param name="id">Payment id</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            base.Logger.LogInformation($"Sending {nameof(GetPaymentQuery)}.");

            var handlerResponse = await Mediator.Send(new GetPaymentQuery(id));

            base.Logger.LogInformation($"{nameof(GetPaymentQuery)} completed with status code {handlerResponse.StatusCode}.");

            return handlerResponse;
        }

        /// <summary>
        /// Create payment
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreatePaymentCommand command)
        {
            base.Logger.LogInformation($"Sending {nameof(CreatePaymentCommand)}.");

            var handlerResponse = await base.Mediator.Send(command);

            base.Logger.LogInformation($"{nameof(CreatePaymentCommand)} completed with status code {handlerResponse.StatusCode}.");

            return handlerResponse;
        }
    }
}
