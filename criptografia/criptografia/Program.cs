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
        static async Task Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            await Animacao("SEJA BEM VINDO A CRIPTOGRAFIA DE HILL!", 20);
            Console.ResetColor();
            Console.WriteLine("\nAperta uma tecla para continuar");
            Console.ReadKey();
            Console.Clear();
            Console.Write("\nDigite a palavra: ");
            string palavra = Validacao(); //chama a função para garantir que escreva letras

            int indice = 0;
            Console.WriteLine("\nTabela ASCII");
            foreach (char letra in palavra) //pega um índice de palavras e atribui a letra
            {
                indice = char.ToLower(letra) - 'a' + 1; // 'a' vale 97 na tabela ASCII. Então: letra - 97 + 1. ex: 'a'(97) - 'a'(97) = 0 + 1 = 1
                Console.WriteLine($"{char.ToUpper(letra)} = {indice}");
            }
            int tamanho = Tamanhomatrizes(palavra.Length); //dimensão da matriz. exp =3x3 = 3, 4x4 = 4...

            string palavraCompleta = CompletarMatriz(palavra, tamanho);

            char[,] Matriz = new char[tamanho, tamanho];

             indice = 0;
            for (int i = 0; i < tamanho; i++)
            {
                for (int j = 0; j < tamanho; j++)
                {
                    Matriz[i, j] = palavraCompleta[indice];
                    indice++;

                    Console.Write($"\t{Matriz[i, j]}");
                }
                Console.WriteLine("");
            }



        }

        static async Task Animacao(string texto, int velocidade)
        {
            foreach (char letra in texto)
            {
                Console.Write(letra);
                await Task.Delay(velocidade);
            }
            Console.WriteLine();
        }

        static string Validacao()
        {
            bool validacao = false;
            string frase = "";

            while (!validacao)
            {
                frase = Console.ReadLine();
                validacao = true; // assume válida
                if (string.IsNullOrEmpty(frase)) //se escrever nada ou um espaço
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Digite pelo menos uma letra: ");
                    Console.ResetColor();
                }
                else
                {
                    validacao = true;

                    foreach (char apoio in frase)
                    {
                        if (!char.IsLetter(apoio)) //se apoio for diferente de letra
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("Digite apenas letras: ");
                            Console.ResetColor();
                            validacao = false; // achou erro, repete
                            break;
                        }
                    }
                }
            }
            return frase;
        }

        static int Tamanhomatrizes(int qtd) //função para calcular o tamanho da matriz apartir do indice da frase
        {
            int tamanho = 0;

            if (qtd <= 4)
            {
                tamanho = 2;
            }
            else if (qtd <= 9)
            {
                tamanho = 3;
            }
            else if (qtd < 16)
            {
                tamanho = 4;
            }
            else if (qtd <= 25)
            {
                tamanho = 5;
            }
            return tamanho;
        }

        static string CompletarMatriz(string palavra, int tamanho)
        {
            int resultado = 0;
            int asteriscos = 0;

            resultado = tamanho * tamanho;
            asteriscos = resultado - palavra.Length;

            while (asteriscos > 0)
            {
                palavra += '*';
                asteriscos--;
            }
            return palavra;
        } //os espaços em vazio serão preenchidos com *. exp: BOL --> matriz 2x2 ficando BO/L*
    }
}

