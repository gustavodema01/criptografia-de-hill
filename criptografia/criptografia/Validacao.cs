using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace criptografia
{
    internal class Validacao
    {
        public static string ValidacaoLetras()
        {
            bool validacao = false;
            string frase = "";

            while (!validacao)
            {
                frase = Console.ReadLine();
                validacao = true;

                if (string.IsNullOrEmpty(frase)) //se a frase estiver vazia ou com espaços
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Digite pelo menos uma palavra(sem espaços):");
                    Console.ResetColor();
                    validacao = false; //pra sair do if
                }
                else if (frase.Length > 9) //se a frase tiver mais de 9 letras
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Digite menos de 9 letras: ");
                    Console.ResetColor();
                    validacao = false; // sai do else
                }
                else
                {
                    foreach (char c in frase)
                    {
                        if (!char.IsLetter(c)) //se a frase tiver algo diferente de letras
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("Digite apenas letras(sem espaços): ");
                            Console.ResetColor();
                            validacao = false; // sai do else
                            break;
                        }
                    }
                }
            }
            return frase;
        }
        public static int ValidacaoInt()
        {
            int output = 0;
            bool validacao = false;
            while (!validacao)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out output))
                {
                    validacao = true;
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Insira apenas números: ");
                    Console.ResetColor();
                }
            }
            return output;
        }
    }
}
