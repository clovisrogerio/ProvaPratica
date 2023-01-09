using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("v1/movimento")]
    public class MovimentoController : BaseController
    {
        public MovimentoController(IMediator mediator)
            : base(mediator)
        { }

        [HttpPost]
        [Route("criarmovimento")]
        public IActionResult CriarMovimento(
            [FromBody] CriarMovimentacaoDeContaRequest command
        )
        {
            var result = Mediator.Send(command);
            if (result.Result.FoiSucesso)
                return Ok($"Movimento criado com sucesso, ID:{result.Result.IdMovimento}");
            return BadRequest($"Erro tipo: {result.Result.TipoMensagem}, {result.Result.Mensagem}");          
        }
    }
}