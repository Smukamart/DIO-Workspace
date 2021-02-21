using System;
using System.Globalization;

namespace AccountSolution
{
    public class ContaBancaria
    {
        public int Id { get; private set; }
        private TipoDeConta TipoDeConta { get; set; }
        private string Nome { get; set; }
        private double Saldo { get; set; }
        public double Credito { get; private set; }
        public double LimiteInicial { get; set; }

        public ContaBancaria()
        {
        }

        public ContaBancaria(int id, TipoDeConta tipoDeConta, string nome, double saldo, double credito)
        {
            Id = id;
            TipoDeConta = tipoDeConta;
            Nome = nome;
            Saldo = saldo;
            Credito = credito;
            LimiteInicial = credito;
        }

        public void Deposito(double valor)
        {
            double valorACompletar = 0;
            if (LimiteInicial > Credito)
            {
                valorACompletar = LimiteInicial - Credito;
                valor -= valorACompletar;
                Credito += valorACompletar;
                Saldo = 0;
                Saldo += valor;
            }
            else
            {
                Saldo += valor;
            }
        }

        public void Saque(double valor)
        {
            if (Saldo >= 0 && Saldo >= valor)
            {
                Saldo -= valor;
            }
            else if (Saldo < valor && (Saldo + Credito) >= valor)
            {
                double resto = valor - Saldo;
                Saldo -= valor;
                Credito -= resto;
            }
            else if (Saldo <= 0 && Credito >= valor)
            {
                Saldo -= valor;
                Credito -= valor;
            }
            else
            {
                throw new DomainException("Limite de Crédito Excedido");
            }
        }

        public void Transferencia(ContaBancaria origem, ContaBancaria destino, double valor)
        {
            origem.Saque(valor);
            destino.Deposito(valor);
        }

        public override string ToString()
        {
            return $"Numero: {Id} | Tipo De Conta: {TipoDeConta} | Titular: {Nome} | Saldo: ${Saldo.ToString("F2", CultureInfo.InvariantCulture)} | Crédito Disponivel: ${Credito.ToString("F2", CultureInfo.InvariantCulture)}";
        }
    }
}