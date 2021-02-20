using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AccountSolution
{
    class Program
    {
        static List<ContaBancaria> contasBancarias = new List<ContaBancaria>();
        static string opcaoDoUsuario = IniciaPrograma();
        static void Main(string[] args)
        {
            try
            {
                while (opcaoDoUsuario.ToUpper() != "X")
                {
                    switch (opcaoDoUsuario)
                    {
                        case "1":
                            //Adicionar Conta
                            AdicionarConta();
                            break;
                        case "2":
                            //Listar Conta
                            ListarContas();
                            break;
                        case "3":
                            //Efetuar Depósito
                            EfetuarDeposito();
                            break;
                        case "4":
                            //Efetuar Saque
                            EfetuarSaque();
                            break;
                        case "5":
                            //Efetuar Transferência
                            EfetuarTransferencia();
                            break;
                        case "C":
                            Console.Clear();
                            IniciaPrograma();
                            break;
                        default:
                            throw new DomainException("Opção Inválida.");
                    }
                }
                Console.WriteLine("Obrigado!!! Volte Sempre.");
                Console.Read();
            }
            catch (DomainException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static string IniciaPrograma()
        {
            Console.WriteLine();
            Console.WriteLine("Bem Vindo Ao Banco DIO");
            Console.WriteLine();
            Console.WriteLine("Digite Uma Das Opções Abaixo:");
            Console.WriteLine("1 - Adicionar Conta.");
            Console.WriteLine("2 - Listar Contas");
            Console.WriteLine("3 - Deposito.");
            Console.WriteLine("4 - Saque.");
            Console.WriteLine("5 - Transferência.");
            Console.WriteLine("C - Limpar a Tela");
            Console.WriteLine("X - Sair");
            Console.WriteLine();
            opcaoDoUsuario = Console.ReadLine().ToUpper();
            return opcaoDoUsuario;
        }

        public static void AdicionarConta()
        {
            Console.WriteLine();
            Console.Write("Numero Da Conta: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Tipo De Conta(Digite 1 para pessoa Fisica ou 2 para pessoa juridica): ");
            TipoDeConta tipo = Enum.Parse<TipoDeConta>(Console.ReadLine());
            if (tipo != TipoDeConta.Fisica && tipo != TipoDeConta.Juridica)
            {
                throw new DomainException("Tipo de Conta inválido");
            }
            Console.Write("Nome e Sobrenome: ");
            string nome = Console.ReadLine();
            Console.Write("Saldo: ");
            double saldo = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            Console.Write("Limite de Crédito Desejado: ");
            double credito = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            ContaBancaria conta = new ContaBancaria(id, tipo, nome, saldo, credito);
            contasBancarias.Add(conta);
            Console.WriteLine("Conta Adicionada com sucesso.");
            Console.WriteLine();
            IniciaPrograma();
        }

        public static void ListarContas()
        {
            Console.WriteLine();
            foreach (var item in contasBancarias)
            {
                Console.WriteLine(item);
            }
            IniciaPrograma();
        }

        public static void EfetuarDeposito()
        {
            Console.WriteLine();
            Console.Write("Digite o numero da Conta de Destino: ");
            int num = int.Parse(Console.ReadLine());
            Console.Write("Digite o Valor do Depósito: ");
            double valor = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            foreach (var item in contasBancarias)
            {
                if (item.Id == num)
                {
                    item.Deposito(valor);
                    Console.WriteLine("Depósito efetuado com sucesso.");
                }
            }
            Console.WriteLine("Conta Inexistente, Depósito Não Efetuado");

            IniciaPrograma();
        }

        public static void EfetuarSaque()
        {
            Console.WriteLine();
            Console.Write("Digite o numero da Conta: ");
            int num = int.Parse(Console.ReadLine());
            Console.Write("Digite o Valor do Saque: ");
            double valor = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            foreach (var item in contasBancarias)
            {
                if (item.Id == num)
                {
                    item.Saque(valor);
                    Console.WriteLine("Saque efetuado com sucesso.");
                }
                else
                {
                    Console.WriteLine("Conta Inexistente, Saque Não Efetuado");
                }
            }
            IniciaPrograma();
        }

        public static void EfetuarTransferencia()
        {
            Console.WriteLine();
            Console.Write("Digite o Numero Da Conta De Origem: ");
            int contOrigem = int.Parse(Console.ReadLine());
            Console.Write("Digite o Numero Da Conta de Destino: ");
            int contDestino = int.Parse(Console.ReadLine());
            Console.Write("Valor a Trasnferir: ");
            double valor = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            ContaBancaria origem = null;
            ContaBancaria destino = null;
            foreach (var item in contasBancarias)
            {
                if (item.Id == contOrigem)
                {
                    origem = item;
                }
                else if (item.Id == contDestino)
                {
                    destino = item;
                }
            }
            origem.Transferencia(origem, destino, valor);
            IniciaPrograma();
        }
    }
}
