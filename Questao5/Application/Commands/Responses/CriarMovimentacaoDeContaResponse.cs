using Questao5.Domain.Enumerators;

namespace Questao5.Application.Commands.Responses
{
    public class CriarMovimentacaoDeContaResponse
    {
        public Guid IdMovimento { get; set; }
        public bool FoiSucesso { get; set; }
        public string Mensagem { get; set; }
    }
}
