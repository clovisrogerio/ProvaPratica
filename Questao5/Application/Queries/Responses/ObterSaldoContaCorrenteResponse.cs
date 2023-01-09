namespace Questao5.Application.Queries.Responses
{
    public class ObterSaldoContaCorrenteResponse
    {
        public long Numero { get; set; }
        public string Nome { get; set; }
        public double SaldoAtual { get; set; }
        public DateTime DataConsulta { get; set; }
        public bool FoiSucesso { get; set; }
        public string Mensagem { get; set; }
    }
}
