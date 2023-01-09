﻿using Questao5.Domain.Entities;

namespace Questao5.Domain.Repositories
{
    public interface IContaCorrenteRepository
    {
        List<Movimento> ObterMovimentosDaContaCorrente(string contaCorrenteId);
        ContaCorrente? ObterContaCorrentePeloId(string contaCorrenteId);
    }
}