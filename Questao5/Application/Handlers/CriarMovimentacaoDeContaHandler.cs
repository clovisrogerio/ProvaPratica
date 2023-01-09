using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities;
using Questao5.Domain.Repositories;
using Questao5.Domain.Enumerators;

namespace Questao5.Application.Handlers
{
    public class CriarMovimentacaoDeContaHandler : IRequestHandler<CriarMovimentacaoDeContaRequest, CriarMovimentacaoDeContaResponse>
    {
        IMovimentoRepository _movimentoRepositorio;
        IContaCorrenteRepository _contaCorrenteRepositorio;
        public CriarMovimentacaoDeContaHandler(IMovimentoRepository movimentoRepositorio, IContaCorrenteRepository contaCorrenteRepositorio)
        {
            _movimentoRepositorio = movimentoRepositorio;
            _contaCorrenteRepositorio = contaCorrenteRepositorio;
        }

        public Task<CriarMovimentacaoDeContaResponse> Handle(CriarMovimentacaoDeContaRequest request, CancellationToken cancellationToken)
        {
            CriarMovimentacaoDeContaResponse result;

            if (request.Valor <= 0)
            {
                result = new CriarMovimentacaoDeContaResponse
                {
                    FoiSucesso = false,
                    TipoMensagem = "INVALID_VALUE",
                    Mensagem = "Valor invalido"
                };
                return Task.FromResult(result);
            }

            if (request.TipoMovimento != (char)TipoMovimento.Credito && request.TipoMovimento != (char)TipoMovimento.Debito)
            {
                result = new CriarMovimentacaoDeContaResponse
                {
                    FoiSucesso = false,
                    TipoMensagem = "INVALID_TYPE",
                    Mensagem = "Tipo de movimento invalido, apenas 'C' Credito e 'D' Debito são aceitos"
                };
                return Task.FromResult(result);
            }

            var account = _contaCorrenteRepositorio.ObterContaCorrentePeloId(request.IdContaCorrente);
            if (account == null)
            {
                result = new CriarMovimentacaoDeContaResponse
                {
                    FoiSucesso = false,
                    TipoMensagem = "INVALID_ACCOUNT",
                    Mensagem = $"Conta com id {request.IdContaCorrente} não encontrada"
                };
                return Task.FromResult(result);
            }

            if (!account.Ativo)
            {
                result = new CriarMovimentacaoDeContaResponse
                {
                    FoiSucesso = false,
                    TipoMensagem = "INACTIVE_ACCOUNT",
                    Mensagem = "Conta inativa"
                };
                return Task.FromResult(result);
            }

            var movimento = new Movimento
            {
                IdMovimento = Guid.NewGuid().ToString().ToUpper(),
                DataMovimento = DateTime.Now.ToShortDateString(),
                IdContaCorrente = request.IdContaCorrente,
                TipoMovimento = (TipoMovimento)request.TipoMovimento,
                Valor = request.Valor
            };

            _movimentoRepositorio.Criar(movimento);

            result = new CriarMovimentacaoDeContaResponse
            {
                FoiSucesso = true,
                IdMovimento = movimento.IdMovimento
            };
            return Task.FromResult(result);
        }
    }
}
