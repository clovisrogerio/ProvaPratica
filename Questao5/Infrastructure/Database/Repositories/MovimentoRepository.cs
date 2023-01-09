using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Repositories;
using Questao5.Infrastructure.Sqlite;
using System;

namespace Questao5.Infrastructure.Database.Repositories
{
    public class MovimentoRepository : IMovimentoRepository
    {
        private readonly DatabaseConfig databaseConfig;

        public MovimentoRepository(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public void Criar(Movimento movimento)
        {
            var connection = new SqliteConnection(databaseConfig.Name);
            connection.Open();
            connection.Execute("INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor) " +
                $"Values('{movimento.IdMovimento}', '{movimento.IdContaCorrente}', '{movimento.DataMovimento}', '{(char)movimento.TipoMovimento}', {movimento.Valor})");
            connection.CloseAsync();
        }
    }
}
