using Questao5.Domain.Entities;

namespace Questao5.Domain.Repositories
{
    public interface IMovimentoRepository
    {
        List<Movimento> ObterMovimentosDaContaCorrente(string contaCorrenteId);
        void Criar(Movimento movimento);
    }
}
