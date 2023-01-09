using Questao5.Domain.Entities;

namespace Questao5.Domain.Repositories
{
    public interface IContaCorrenteRepository
    {
        ContaCorrente? ObterContaCorrentePeloId(string contaCorrenteId);
    }
}
