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
            // Aplicar Fail Fast Validations
            if (request.NumeroContaCorrente == 0)
            {
                result = new CriarMovimentacaoDeContaResponse
                {
                    FoiSucesso = false,
                    Mensagem = "INVALID_ACCOUNT"
                };
                return Task.FromResult(result);
            }

            if (request.Valor <= 0)
            {
                result = new CriarMovimentacaoDeContaResponse
                {
                    FoiSucesso = false,
                    Mensagem = "INVALID_VALUE"
                };
                return Task.FromResult(result);
            }

            if (request.TipoMovimento != TipoMovimento.Credito && request.TipoMovimento != TipoMovimento.Debito)
            {
                result = new CriarMovimentacaoDeContaResponse
                {
                    FoiSucesso = false,
                    Mensagem = "INVALID_TYPE"
                };
                return Task.FromResult(result);
            }

            // Check if account exists
            var account = _contaCorrenteRepositorio.ObterContaCorrentePeloId(request.IdContaCorrente);
            if (account == null)
            {
                result = new CriarMovimentacaoDeContaResponse
                {
                    FoiSucesso = false,
                    Mensagem = "INVALID_ACCOUNT"
                };
                return Task.FromResult(result);
            }

            // Check if account is active
            if (!account.Ativo)
            {
                result = new CriarMovimentacaoDeContaResponse
                {
                    FoiSucesso = false,
                    Mensagem = "INACTIVE_ACCOUNT"
                };
                return Task.FromResult(result);
            }

            // Cria a entidade
            var movimento = new Movimento(request.IdContaCorrente, request.TipoMovimento, request.Valor);

            // Persiste a entidade no banco
            _movimentoRepositorio.Criar(movimento);

            // Retorna a resposta
            result =  new CriarMovimentacaoDeContaResponse
            {
                IdMovimento = movimento.IdMovimento
            };
            return Task.FromResult(result);
        }
    }
}
