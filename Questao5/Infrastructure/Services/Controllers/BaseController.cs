using MediatR;
using Microsoft.AspNetCore.Mvc;
using CSharpFunctionalExtensions;

namespace Questao5.Infrastructure.Services.Controllers
{

    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected readonly IMediator Mediator;

        protected BaseController(IMediator mediator)
        {
            Mediator = mediator;
        }
    }
}
