namespace Questao5.Domain.Entities
{
    public class ContaCorrente
    {
        public Guid IdContaCorrente { get; set; }
        public long NumeroContaCorrente { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
    }
}
