using Questao5.Domain.Entities;
using Questao5.Domain.Repositories;

namespace Questao5.Testes.Mocks
{
    public class MockContaCorrenteRepository : IContaCorrenteRepository
    {
        public ContaCorrente? ObterContaCorrentePeloId(string contaCorrenteId)
        {
            return new ContaCorrente();
        }

        public List<Movimento> ObterMovimentosDaContaCorrente(string contaCorrenteId)
        {
            return new List<Movimento>();
        }
    }
}
