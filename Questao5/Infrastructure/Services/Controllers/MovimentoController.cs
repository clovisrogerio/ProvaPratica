using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("v1/movimento")]
    public class MovimentoController : ControllerBase
    {
        [HttpPost]
        [Route("")]
        public Task<CriarMovimentacaoDeContaResponse> Create(
            [FromServices] IMediator mediator,
            [FromBody] CriarMovimentacaoDeContaRequest command
        )
        {
            return mediator.Send(command);
        }
    }
}