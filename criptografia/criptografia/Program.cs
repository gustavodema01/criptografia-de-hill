using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace criptografia
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("SEJA BEM-VINDO(A) A CRIPTOGRAFIA DE HILL!");
            Console.ResetColor();
            Console.WriteLine("\nAperta uma tecla para continuar");
            Console.ReadKey();
            Console.Clear();
            Console.Write("\nDigite a palavra: ");
            string palavra = Validacao(); //chama a função para garantir que escreva letras

            int indice = 0;
            foreach (char letra in palavra) //pega um índice de palavras e atribui a letra
            {
                indice = char.ToLower(letra) - 'a' + 1; // 'a' vale 97 na tabela ASCII. Então: letra - 97 + 1. ex: 'a'(97) - 'a'(97) = 0 + 1 = 1
                Console.WriteLine($"{char.ToUpper(letra)} = {indice}");
            }
        }

        static string Validacao()
        {
            bool validacao = false;
            string frase = "";

            while (!validacao)
            {
                frase = Console.ReadLine();
                validacao = true; // assume válida

                foreach (char apoio in frase)
                {
                    if (!char.IsLetter(apoio))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Digite apenas letras: ");
                        Console.ResetColor();
                        validacao = false; // achou erro, repete
                        break;
                    }
                }
            }

            return frase;
        }
    }
}

