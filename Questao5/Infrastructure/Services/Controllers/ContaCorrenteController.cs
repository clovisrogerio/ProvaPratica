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

        [HttpGet]
        [Route("obtersaldo")]
        public IActionResult ObterSaldo(
            [FromQuery] ObterSaldoContaCorrenteRequest command
        )
        {
            var result = Mediator.Send(command);
            if (result.Result.FoiSucesso)
                return Ok($"Numero da conta: {result.Result.Numero}, Nome do Titular: {result.Result.Nome}, Data da consulta: {result.Result.DataConsulta}, Saldo Atual:{result.Result.SaldoAtual}");
            return BadRequest($"Erro tipo: {result.Result.TipoMensagem}, {result.Result.Mensagem}");
        }
    }
}
