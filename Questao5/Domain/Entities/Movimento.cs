using Questao5.Domain.Enumerators;

namespace Questao5.Domain.Entities
{
    public class Movimento
    {
        public Movimento(Guid idContaCorrente, TipoMovimento tipoMovimento, double valor)
        {
            IdContaCorrente = idContaCorrente;
            IdMovimento = Guid.NewGuid();
            DataMovimento = DateTime.Now.ToShortDateString();
            TipoMovimento = tipoMovimento;
            Valor = valor;
        }

        public Guid IdMovimento { get; set; }
        public Guid IdContaCorrente { get; set; }
        public string DataMovimento { get; set; }
        public TipoMovimento TipoMovimento { get; set; }
        public double Valor { get; set; }
    }
}
