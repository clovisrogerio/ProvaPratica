using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Repositories;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.Repositories
{
    public class ContaCorrenteRepository : IContaCorrenteRepository
    {
        private readonly DatabaseConfig databaseConfig;

        public ContaCorrenteRepository(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public ContaCorrente? ObterContaCorrentePeloId(string contaCorrenteId)
        {
            var connection = new SqliteConnection(databaseConfig.Name);
            connection.Open();
            var contaCorrente = connection.Query<ContaCorrente>
                (
                    $"SELECT * FROM contacorrente WHERE idcontacorrente = '{contaCorrenteId.ToUpper()}'"
                )
                .FirstOrDefault();
            connection.CloseAsync();
            return contaCorrente;
        }
    }
}


