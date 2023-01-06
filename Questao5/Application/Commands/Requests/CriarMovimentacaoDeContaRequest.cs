using Questao5.Domain.Enumerators;

namespace Questao5.Application.Commands.Requests
{
    public class CriarMovimentacaoDeContaRequest
    {
        public Guid IdContaCorrente { get; set; }
        public TipoMovimento TipoMovimento { get; set; }
        public double Valor { get; set; }
    }
}
