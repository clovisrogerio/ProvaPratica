using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Repositories;

namespace Questao5.Application.Handlers
{
    public class ObterSaldoContaCorrenteHandler : IRequestHandler<ObterSaldoContaCorrenteRequest, ObterSaldoContaCorrenteResponse>
    {
        IMovimentoRepository _movimentoRepositorio;
        IContaCorrenteRepository _contaCorrenteRepositorio;
        public ObterSaldoContaCorrenteHandler(IMovimentoRepository movimentoRepositorio, IContaCorrenteRepository contaCorrenteRepositorio)
        {
            _movimentoRepositorio = movimentoRepositorio;
            _contaCorrenteRepositorio = contaCorrenteRepositorio;
        }

        public Task<ObterSaldoContaCorrenteResponse> Handle(ObterSaldoContaCorrenteRequest request, CancellationToken cancellationToken)
        {
            ObterSaldoContaCorrenteResponse result;

            var account = _contaCorrenteRepositorio.ObterContaCorrentePeloId(request.IdContaCorrente);
            if (account == null)
            {
                result = new ObterSaldoContaCorrenteResponse
                {
                    FoiSucesso = false,
                    Mensagem = "INVALID_ACCOUNT"
                };
                return Task.FromResult(result);
            }

            // Check if account is active
            if (!account.Ativo)
            {
                result = new ObterSaldoContaCorrenteResponse
                {
                    FoiSucesso = false,
                    Mensagem = "INACTIVE_ACCOUNT"
                };
                return Task.FromResult(result);
            }

            var movimentosDaContaCorrente = _contaCorrenteRepositorio.ObterMovimentosDaContaCorrente(request.IdContaCorrente);
            var saldoFinal = 0.0;
            foreach (var movimentos in movimentosDaContaCorrente)
            {
                switch (movimentos.TipoMovimento)
                {
                    case TipoMovimento.Debito:
                        saldoFinal -= movimentos.Valor;
                        break;

                    case TipoMovimento.Credito:
                        saldoFinal += movimentos.Valor;
                        break;
                }                  
            }

            // Retorna a resposta
            result = new ObterSaldoContaCorrenteResponse
            {
                FoiSucesso = true,
                SaldoAtual = saldoFinal,
                DataConsulta = DateTime.Now,
                Nome = account.Nome,
                Numero = account.NumeroContaCorrente
            };

            return Task.FromResult(result);
        }
    }

}
