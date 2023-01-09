using MediatR;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Enumerators;

namespace Questao5.Application.Commands.Requests
{
    public class CriarMovimentacaoDeContaRequest : IRequest<CriarMovimentacaoDeContaResponse>
    {
        public Guid IdContaCorrente { get; set; }
        public long NumeroContaCorrente { get; set; }
        public TipoMovimento TipoMovimento { get; set; }
        public double Valor { get; set; }
    }
}
