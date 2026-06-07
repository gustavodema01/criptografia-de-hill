using System;
using System.Threading.Tasks;

namespace criptografia
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            bool sair = false;
            while (!sair)
            {
                int opcao = await TelaMenu.Menu();
                Console.Clear();

                if (opcao == 1) Operações.Criptografar();
                else if (opcao == 2) Operações.Descriptografar();
                else if (opcao == 3) TelaMenu.ComoFunciona();
                else if (opcao == 4) TelaMenu.ChaveComoFunciona();

                Console.WriteLine("\nDeseja voltar ao Menu?");
                Console.WriteLine("1. SIM");
                Console.WriteLine("2. NÃO");
                Console.Write("O QUE DESEJA?:");

                bool validacao = false;
                while (!validacao)
                {
                    int escolha = Validacao.ValidacaoInt();
                    if (escolha == 1)  validacao = true;
                    else if (escolha == 2)
                    {
                        Console.Clear();
                        Console.WriteLine("Saindo...");
                        sair = true;
                        validacao = true;
                    }
                    else              
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Escolha entre 1 e 4");
                }
                Console.Clear();
            }
        }

    }
}






