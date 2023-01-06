using Castle.Core.Resource;
using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities;
using Questao5.Domain.Repositories;

namespace Questao5.Application.Handlers
{
    public class CriarMovimentacaoDeContaHandler : IRequestHandler<CriarMovimentacaoDeContaRequest, CriarMovimentacaoDeContaResponse>
    {
        IMovimentoRepository _repositorio;
        public CriarMovimentacaoDeContaHandler(IMovimentoRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public Task<CriarMovimentacaoDeContaResponse> Handle(CriarMovimentacaoDeContaRequest request, CancellationToken cancellationToken)
        {
            // Aplicar Fail Fast Validations

            // Cria a entidade
            var movimento = new Movimento(request.IdContaCorrente, request.TipoMovimento, request.Valor);

            // Persiste a entidade no banco
            _repositorio.Criar(movimento);

            // Retorna a resposta
            var result =  new CriarMovimentacaoDeContaResponse
            {
                IdMovimento = movimento.IdMovimento
            };

            return Task.FromResult(result);
        }
    }
}
