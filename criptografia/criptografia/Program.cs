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
            await Menu(); //sem o await apareceria o write de baixo junto com a função
            Console.Clear();

            Console.Write("\nDigite a palavra: ");
            string palavra = Validacao(); //chama a função Validacao para garantir que escreva letras

            int indice = 0;
            Console.WriteLine("\nTabela ASCII");
            foreach (char letra in palavra) //pega um índice de palavra e atribui a letra
            {
                indice = char.ToLower(letra) - 'a' + 1; // 'a' vale 97 na tabela ASCII. Então: letra - 97 + 1. ex: 'a'(97) - 'a'(97) = 0 + 1 = 1
                Console.WriteLine($"{char.ToUpper(letra)} = {indice}"); //essa letra é esse número
            }
            int tamanho = Tamanhomatrizes(palavra.Length); //dimensão da matriz de acordo com a quantidade de letras em palavra. exp =3x3 = 3, 4x4 = 4...

            string palavraCompleta = CompletarMatriz(palavra, tamanho); //palavra com os *, se necessário

            char[,] Matriz = new char[tamanho, tamanho];

            Console.Clear();
            indice = 0;
            Console.WriteLine("Matriz em letras: "); //colocar isso em função

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
            int[,] MatrizNum = new int[tamanho, tamanho];
            indice = 0;
            Console.WriteLine("Matriz em números: ");   //colocar isso em função

            for (int i = 0; i < tamanho; i++)
            {
                for (int k = 0; k < tamanho; k++)
                {
                    MatrizNum[i, k] = char.ToLower(palavraCompleta[indice]) - 'a' + 1;
                    indice++;
                    if (MatrizNum[i, k] == -54) // o valor do * na tabela ASCII é -54, mas no alfabeto de hill é 0. então atualizei o valor para 0 na matriz
                    {
                        MatrizNum[i, k] = 0;
                    }
                    Console.Write($"\t{MatrizNum[i, k]}");
                }
                Console.WriteLine("");
            }
            Console.WriteLine("\nAperte qualquer tecla para seguir em frente");
            Console.ReadKey();

            Console.Clear();
            int[,] Chave = MatrizChave(tamanho);

        }

        static async Task Menu()//se tem await, a função precisa ser asynk task ao invés de void
        {
            Console.ForegroundColor = ConsoleColor.Green;
            await Animacao("SEJA BEM VINDO A CRIPTOGRAFIA DE HILL!", 20);
            Console.ResetColor();
            Console.WriteLine("\nAperta uma tecla para continuar");
            Console.ReadKey();
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
        static int ValidacaoInt()
        {
            int output = 0;
            bool validacao = false;
            while (!validacao)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out output))
                {
                    validacao = true;
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
                palavra += ('*');
                asteriscos--;
            }
            return palavra;
        } //os espaços em vazio serão preenchidos com *. exp: BOL --> matriz 2x2 ficando BO/L*
        static int[,] MatrizChave(int tamanho) //função para fazer a matriz chave(C) 
        {
            Console.WriteLine("Digite a Matriz Chave: ");
            int[,] Chave = new int[tamanho, tamanho]; //tamanho = tamanho da matriz

            for (int i = 0; i < tamanho; i++)
            {
                for (int j = 0; j < tamanho; j++)
                {
                    Console.Write($"[{i}],[{j}]:");
                    Chave[i, j] = ValidacaoInt(); //função que garante que o usuário só escreva números
                }
            }
            Console.Clear();
            Console.WriteLine("Sua Matriz Chave:");
            for (int i = 0; i < tamanho; i++)
            {
                for (int j = 0; j < tamanho; j++)
                {
                    Console.Write($"\t{Chave[i, j]}");
                }
                Console.WriteLine("");
            }

            Console.WriteLine("\nOBS: NÃO ESQUEÇA A MATRIZ CHAVE.");
            Console.WriteLine("\n\nAperte qualquer tecla para seguir em frente");
            Console.ReadKey();
            return Chave;
        }
    }
}

