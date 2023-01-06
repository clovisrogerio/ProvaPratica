using System.Globalization;

namespace Questao1
{
    class ContaBancaria
    {
        static private double TaxaDeSaque = 3.50;
        public long NumeroDaConta { get; set; }
        public string NomeTitular { get; set; }
        public double Saldo { get; private set; }

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

        public override string ToString()
        {
            return $"Conta {NumeroDaConta}, Titular: {NomeTitular}, Saldo: $ {Saldo.ToString("0.00", CultureInfo.InvariantCulture)}";
        }
    }
}
