using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PaymentsAPI.Controllers
{
    public abstract class BaseController<T> : ControllerBase where T: class
    {
        public readonly IMediator Mediator;
        public readonly ILogger<T> Logger;

        protected BaseController(IMediator mediator, ILogger<T> logger)
        {
            Mediator = mediator;
            Logger = logger;
        }
    }
}