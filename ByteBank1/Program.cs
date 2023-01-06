using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace ByteBank1
{

    public class Program
    {

        static void ShowMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("-----------------------------");
            Console.WriteLine("- Bem vindo(a) ao BYTE BANK -");
            Console.WriteLine("-----------------------------");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("  1 - Inserir novo usuário");
            Console.WriteLine("  2 - Deletar um usuário");
            Console.WriteLine("  3 - Listar todas as contas registradas");
            Console.WriteLine("  4 - Detalhes de um usuário");
            Console.WriteLine("  5 - Quantia armazenada no banco");
            Console.WriteLine("  6 - Manipular a conta");
            Console.WriteLine("  0 - Para sair do programa");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Digite a opção desejada: ");
            Console.ResetColor();
        }

        static void RegistrarNovoUsuario(List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos, List<string> historico)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Digite seu CPF: ");
            Console.ForegroundColor = ConsoleColor.White;
            cpfs.Add(Console.ReadLine());

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Digite seu nome: ");
            Console.ForegroundColor = ConsoleColor.White;
            titulares.Add(Console.ReadLine());

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Digite sua senha: ");
            Console.ForegroundColor = ConsoleColor.White;
            senhas.Add(Console.ReadLine());
            saldos.Add(0);
            historico.Add(null);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Usuário cadastrado com sucesso!");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Pressione a tecla ENTER para continuar!");
            Console.ReadKey();
            Console.ResetColor();
        }

        static void DeletarUsuario(List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos, List<string> historico)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Digite seu CPF: ");
            Console.ForegroundColor = ConsoleColor.White;
            string cpfParaDeletar = Console.ReadLine();
            Console.ResetColor();
            int indexParaDeletar = cpfs.FindIndex(cpf => cpf == cpfParaDeletar);

            if (indexParaDeletar == -1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Não foi possível deletar esta Conta");
                Console.WriteLine("MOTIVO: Conta não encontrada.");
                Console.ResetColor();
            }

            cpfs.Remove(cpfParaDeletar);
            titulares.RemoveAt(indexParaDeletar);
            senhas.RemoveAt(indexParaDeletar);
            saldos.RemoveAt(indexParaDeletar);
            historico.RemoveAt(indexParaDeletar);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Conta deletada com sucesso");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Pressione a tecla ENTER para continuar!");
            Console.ReadKey();
        }

        static void ListarTodasAsContas(List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            Console.Clear();
            for (int i = 0; i < cpfs.Count; i++)
            {
                ApresentaConta(i, cpfs, titulares, saldos);
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Pressione a tecla ENTER para continuar!");
            Console.ReadKey();
            Console.ResetColor();
        }

        static void ApresentarUsuario(List<string> cpfs, List<string> titulares, List<double> saldos, List<string> senhas)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Digite seu CPF: ");
            Console.ForegroundColor = ConsoleColor.White;
            string cpfParaApresentar = Console.ReadLine();

            int index = cpfs.FindIndex(cpf => cpf == cpfParaApresentar);
            bool conta = false;
            conta = VerificarConta(index, titulares, saldos, senhas);

            if (conta == true)
            {
                ApresentaConta(index, cpfs, titulares, saldos);
            }
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Pressione a tecla ENTER para continuar!");
            Console.ReadKey();
            Console.ResetColor();
        }

        static void ApresentarValorAcumulado(List<double> saldos)
        {
            Console.Clear();
            double saldoFinal = saldos.Sum();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Total acumulado no banco: R$ {saldoFinal.ToString("F2")}");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Pressione a tecla ENTER para continuar!");
            Console.ReadKey();
            Console.ResetColor();
        }

        static void ApresentaConta(int index, List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"CPF = {cpfs[index]} | Titular = {titulares[index]} | Saldo = R${saldos[index]:F2}");
            Console.ResetColor();
        }

        static bool VerificarConta(int index, List<string> titulares, List<double> saldos, List<string> senhas)
        {
            Console.Clear();
            string senha;
            bool c1 = false, contaFinal = false;

            if (index == -1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Não foi possível realizar o depósito");
                Console.WriteLine("Sua conta não foi encontrada.");
                Console.ResetColor();
                return false;
            }
            else
            {
                int aux = 0;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("Digite sua senha. Você tem apenas 3 'TRÊS' tentativas: ");

                Console.ForegroundColor = ConsoleColor.White;
                senha = Console.ReadLine();
                Console.ResetColor();
                while (c1 == false)
                {
                    if (senhas[index] != senha)
                    {
                        aux++;
                        if (aux <= 2)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Senha incorreta!, retam {3 - aux} tentativas.");
                            Console.WriteLine($"Restam {3 - aux} tentativas.");

                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("Digite sua senha novamente: ");

                            Console.ForegroundColor = ConsoleColor.White;
                            senha = Console.ReadLine();
                            Console.ResetColor();
                        }
                        else if (aux == 3)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Você foi desconectado. Faça login novamente!");
                            Console.ResetColor();
                            c1 = true;
                            contaFinal = false;
                        }

                    }
                    else
                    {
                        Console.WriteLine($"Seja bem vindo(a) {titulares[index]} !");
                        c1 = true;
                        contaFinal = true;
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Pressione a tecla ENTER para continuar!");
            Console.ReadKey();
            Console.ResetColor();
            return contaFinal;
        }

    static void RealizarDeposito(List<string> cpfs, List<string> titulares, List<double> saldos, List<string> senhas, List<string> historico)
        {
            Console.Clear();
            bool conta;

            Console.ForegroundColor = ConsoleColor.Red;           
            Console.Write("Digite seu CPF: ");

            Console.ForegroundColor = ConsoleColor.White;
            string cpfParaApresentar = Console.ReadLine();
            int index = cpfs.FindIndex(cpf => cpf == cpfParaApresentar);
            double valor = 0;
            conta = VerificarConta(index, titulares, saldos, senhas);
            Console.ResetColor();

            if (conta == true)
            {    
                Console.ForegroundColor = ConsoleColor.Green;           
                Console.Write("Digite o valor que deseja depositar em sua conta: ");

                Console.ForegroundColor = ConsoleColor.White;
                valor = double.Parse(Console.ReadLine());
                Console.ResetColor();
                saldos[index] += valor;
                Console.ForegroundColor = ConsoleColor.Green;
                string deposito = ($"Depósito realizado no valor de R$ {valor.ToString("F2")}");
                Console.WriteLine(deposito);                
                historico[index] += ($"\n {deposito} \n");
                Console.ResetColor();
            }
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Pressione a tecla ENTER para continuar!");
            Console.ReadKey();
            Console.ResetColor();
        }

        static void RealizarSaque(List<string> cpfs, List<string> titulares, List<double> saldos, List<string> senhas, List<string> historico)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Digite seu CPF: ");
            Console.ForegroundColor = ConsoleColor.White;
            string cpfParaApresentar = Console.ReadLine();
            Console.ResetColor();
            int index = cpfs.FindIndex(cpf => cpf == cpfParaApresentar);
            bool conta;
            double valor = 0;
            conta = VerificarConta(index, titulares, saldos, senhas);          

                if (conta == true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Digite o valor a ser sacado em conta: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    valor = double.Parse(Console.ReadLine());
                    Console.ResetColor();
                }
                try
                {
                    if (saldos[index] < valor)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Não foi possível realizar o saque!");
                        Console.WriteLine("Seu saldo está menor que o valor solicitado para saque!");
                        Console.ResetColor();
                    }
                    else
                    {
                        string saque;
                        saldos[index] -= valor;
                        Console.ForegroundColor = ConsoleColor.Green;
                        saque = ($"Saque realizado no valor de R$ {valor.ToString("F2"/*, CultureInfo.InvariantCulture*/)}");
                        Console.WriteLine(saque);
                        historico[index] += ($"\n{saque} \n");
                        Console.ResetColor();
                    }
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("CPF inválido!");
                    Console.ResetColor();                   
                }

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Pressione a tecla ENTER para continuar!");
            Console.ReadKey();
            Console.ResetColor();
        }
        static void RealizarTransferencia(List<string> cpfs, List<string> titulares, List<double> saldos, List<string> senhas, List<string> historico)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Digite o CPF da conta para qual deseja realizar a transferência: ");
            Console.ForegroundColor = ConsoleColor.White;
            string cpfParaApresentar = Console.ReadLine();
            Console.ResetColor();
            int index = cpfs.FindIndex(cpf => cpf == cpfParaApresentar), index2=-1;
            bool conta, contaDestinatario = false;
            conta = VerificarConta(index, titulares, saldos, senhas);
            double valor = 0;
            if (conta == true)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Digite o CPF da conta que receberá a transferência:");
                Console.ForegroundColor = ConsoleColor.White;
                string cpfParaApresentar2 = Console.ReadLine();
                Console.ResetColor();
                index2 = cpfs.FindIndex(cpf => cpf == cpfParaApresentar2);


                if (index2 == -1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Não foi possível realizar a transferência!");
                    Console.WriteLine("A conta do destinatário não foi encontrada.");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Digite o valor a ser transferido para a conta: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    valor = double.Parse(Console.ReadLine());
                    Console.ResetColor();
                    contaDestinatario = true;
                }

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Pressione a tecla ENTER para continuar!");
                Console.ReadKey();
                Console.ResetColor();

            }

            try
            {
                if (saldos[index] < valor && contaDestinatario == true)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Não foi possível realizar a transferencia");
                    Console.WriteLine("Seu saldo está a baixo do valor solicitado para transferencia.");
                    Console.ResetColor();
                }
                else if (saldos[index] > valor && contaDestinatario == true)
                {
                    saldos[index] -= valor;
                    saldos[index2] += valor;
                    Console.ForegroundColor = ConsoleColor.Green;
                    string transferencia = ($"Transferencia realizada no valor de R$ {valor.ToString("F2")}");
                    Console.WriteLine(transferencia);
                    Console.ForegroundColor = ConsoleColor.White;
                    historico[index] += ($"\n {transferencia} \n");
                    Console.ForegroundColor = ConsoleColor.Green;
                    historico[index2] += ($"\n Transferencia recebida no valor de R${valor.ToString("F2")} \n");
                    Console.ResetColor();
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("CPF Inválido!");
                Console.ResetColor();
            }
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Pressione a tecla ENTER para continuar!");
            Console.ReadKey();
            Console.ResetColor();
        }
        
        static void ExtratoConta(List<string> cpfs, List<string> titulares, List<double> saldos, List<string> senhas, List<string> historico)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Digite o CPF da conta para qual será realizado o extrato: ");
            Console.ForegroundColor = ConsoleColor.White;
            string cpfParaApresentar = Console.ReadLine();
            Console.ResetColor();
            int index = cpfs.FindIndex(cpf => cpf == cpfParaApresentar);

            bool conta = false;
            conta = VerificarConta(index, titulares, saldos, senhas);
            if (conta == true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(historico[index]);
                Console.ResetColor();
            }
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Pressione a tecla ENTER para continuar!");
            Console.ReadKey();
            Console.ResetColor();
        }

        static void MenuSecundario(List<string> cpfs, List<string> titulares, List<double> saldos, List<string> senhas, List<string> historico)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Bem vindo ao menu de operações!");
            Console.WriteLine("Digite a opção desejada: ");
            Console.ResetColor();
            int option = 0;
            
            while (option != 9)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("  1 - Realizar um depósito");
                Console.WriteLine("  2 - Realizar um saque");
                Console.WriteLine("  3 - Realizar uma transfêrencia");
                Console.WriteLine("  4 - Mostrar extrato");
                Console.WriteLine("  9 - Voltar ao menu anterior");

                Console.ForegroundColor = ConsoleColor.White;
                    option = int.Parse(Console.ReadLine());
                    Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine("-----------------");
                        Console.ResetColor();
                        switch (option)
                        {
                            case 9:
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine("Pressione a tecla ENTER para continuar!");
                                Console.ResetColor();
                                break;
                            case 1:
                                RealizarDeposito(cpfs, titulares, saldos, senhas, historico);
                                break;
                            case 2:
                                RealizarSaque(cpfs, titulares, saldos, senhas, historico);
                                break;
                            case 3:
                                RealizarTransferencia(cpfs, titulares, saldos, senhas, historico);
                                break;
                            case 4:
                                ExtratoConta(cpfs, titulares, saldos, senhas, historico);
                                break;
                        }               
            }
            Console.ReadKey();
        }
        public static void Main(string[] args)
        {
            List<string> cpfs = new List<string>();
            List<string> titulares = new List<string>();
            List<string> senhas = new List<string>();
            List<double> saldos = new List<double>();
            List<string> historico = new List<string>();

            int option;

            do
            {
                ShowMenu();
                Console.ForegroundColor = ConsoleColor.White;
                option = int.Parse(Console.ReadLine());                

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("-----------------");

                switch (option)
                {
                    case 0:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Programa Encerrado!");
                        Console.ResetColor();
                        break;
                    case 1:
                        RegistrarNovoUsuario(cpfs, titulares, senhas, saldos, historico);
                        break;
                    case 2:
                        DeletarUsuario(cpfs, titulares, senhas, saldos, historico);
                        break;
                    case 3:
                        ListarTodasAsContas(cpfs, titulares, saldos);
                        break;
                    case 4:
                        ApresentarUsuario(cpfs, titulares, saldos, senhas);
                        break;
                    case 5:
                        ApresentarValorAcumulado(saldos);
                        break;
                    case 6:
                        MenuSecundario(cpfs, titulares, saldos, senhas, historico);
                        break;
                }
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("-----------------");
                Console.ResetColor();

            } while (option != 0);
        }
    }
}