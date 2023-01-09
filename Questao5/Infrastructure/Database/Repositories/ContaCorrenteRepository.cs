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


        public List<Movimento> ObterMovimentosDaContaCorrente(string contaCorrenteId)
        {
            var listaDeMovimentos = new List<Movimento>();
            var connection = new SqliteConnection(databaseConfig.Name);
            connection.Open();
            using (var reader = connection.ExecuteReader($"SELECT tipomovimento, valor FROM movimento WHERE idcontacorrente = '{contaCorrenteId.ToUpper()}'"))
            {
                while (reader.Read())
                {
                    var movimento = new Movimento();

                    movimento.TipoMovimento = (TipoMovimento)Convert.ToChar(reader["tipomovimento"]);
                    movimento.Valor = Convert.ToDouble(reader["valor"]);

                    listaDeMovimentos.Add(movimento);
                }
            }
                
            connection.CloseAsync();
            return listaDeMovimentos;
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


