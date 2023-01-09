using Questao5.Domain.Entities;
using Questao5.Domain.Repositories;

namespace Questao5.Testes.Mocks
{
    public class MockMovimentoRepository : IMovimentoRepository
    {

        public void Criar(Movimento movimento)
        {
        }

        public List<Movimento> ObterMovimentosDaContaCorrente(string contaCorrenteId)
        {
            throw new NotImplementedException();
        }
    }
}
