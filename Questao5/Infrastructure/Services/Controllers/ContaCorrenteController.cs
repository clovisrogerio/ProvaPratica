using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Queries.Requests;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("v1/contacorrente")]
    public class ContaCorrenteController : BaseController
    {
        public ContaCorrenteController(IMediator mediator)
            : base(mediator)
        { }

        [HttpPost]
        [Route("")]
        public IActionResult Create(
            [FromBody] ObterSaldoContaCorrenteRequest command
        )
        {
            var result = Mediator.Send(command);
            if (result.Result.FoiSucesso)
                return Ok(result);
            return BadRequest(result.Result.Mensagem);
        }
    }
}
