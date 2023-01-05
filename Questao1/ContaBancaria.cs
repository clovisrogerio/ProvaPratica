namespace Questao1
{
    class ContaBancaria
    {
        static private double TaxaDeSaque = 3.50;
        public long NumeroDaConta { get; set; }
        public string NomeTitular { get; set; }
        private double Saldo { get; set; }


        public ContaBancaria(long numeroDaConta, string nomeTitular, double saldo = 0)
        {
            NumeroDaConta = numeroDaConta;
            NomeTitular = nomeTitular;
            Saldo = saldo;
        }

        public void Saque(double sacar)
        {
            this.Saldo -= sacar + TaxaDeSaque;
        }

        public void Deposito(double deposito)
        {
            this.Saldo += deposito;
        }

        public string MostrarDados()
        {
            return $"Conta {NumeroDaConta}, Titular: {NomeTitular}, Saldo: $ {Saldo:0.00}";
        }
    }
}
