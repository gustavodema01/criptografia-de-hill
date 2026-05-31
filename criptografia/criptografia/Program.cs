using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace criptografia
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            int opcao = await Menu(); //sem o await apareceria o write de baixo junto com a função
            Console.Clear();

            if (opcao == 1)
            {
                await Criptografar();
            }
            else if (opcao == 2)
            {
                Descriptografia();
            }
        }

        static async Task<int> Menu()//se tem await, a função precisa ser asynk task ao invés de void
        {
            Console.ForegroundColor = ConsoleColor.Green;
            await Animacao("SEJA BEM VINDO A CRIPTOGRAFIA DE HILL!", 20);
            Console.ResetColor();
            Console.WriteLine("\n1. Criptografar uma frase");
            Console.WriteLine("2. Descriptografar uma frase");
            Console.Write("O que deseja?: ");
            bool validacao = false;
            int escolhamenu = 0;
            while (!validacao)
            {
                escolhamenu = ValidacaoInt();
                validacao = true;

                if (escolhamenu <= 0 | escolhamenu > 2)
                {
                    Console.Clear();
                    Console.ForegroundColor= ConsoleColor.Green;
                    Console.WriteLine("SEJA BEM VINDO A CRIPTOGRAFIA DE HILL!");
                    Console.ResetColor();

                    Console.WriteLine("\n1. Criptografar uma frase");
                    Console.WriteLine("2. Descriptografar uma frase");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Escolha uma opção disponível: ");
                    Console.ResetColor();
                    validacao = false;
                }
            }
            Console.WriteLine("\nAperta uma tecla para continuar");
            Console.ReadKey();
            return escolhamenu;
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
            if (qtd <= 4) return 2;
            if (qtd <= 9) return 3;
            return 0; // inválido

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
        static char[,] MatrizLetras(int largura, char[,] Matriz, string todapalavra, int indice)
        {
            for (int i = 0; i < largura; i++)
            {
                for (int j = 0; j < largura; j++)
                {
                    Matriz[i, j] = todapalavra[indice];
                    indice++;

                    Console.Write($"\t{Matriz[i, j]}");
                }
                Console.WriteLine("");
            }
            return Matriz;
        }
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
            Console.WriteLine("Aperte qualquer tecla para continuar");
            Console.ReadKey();
            return Chave;
        }
        static int[,] Criptografada(int[,] matrizpalavra, int[,] matrizchave, int tamanho, string verso)
        {
            Console.Clear();
            int[,] resultado = new int[tamanho, tamanho];

            for (int i = 0; i < tamanho; i++)
            {
                for (int j = 0; j < tamanho; j++)
                {
                    resultado[i, j] = 0;
                    for (int k = 0; k < tamanho; k++) // linha 0 da coluna 0 da multiplicação de matrizes é o indice 0(k)
                    {
                        resultado[i, j] += matrizchave[i, k] * matrizpalavra[k, j];
                    }
                    resultado[i, j] = resultado[i, j] % 26;
                }
            }
            Console.Write("Palavra: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(verso.ToUpper());
            Console.ResetColor();
            Console.WriteLine(" Criptografada: ");

            for (int i = 0; i < tamanho; i++)
            {
                for (int j = 0; j < tamanho; j++)
                {
                    Console.Write($"\t{resultado[i, j]}");
                }
                Console.WriteLine("");
            }
            return resultado;
        }
        static async Task<int[,]> Criptografar()
        {
            Console.Write("Digite a palavra(até 9 letras): ");
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
            Console.WriteLine("Matriz em letras: ");
            char[,] letras = MatrizLetras(tamanho, Matriz, palavraCompleta, indice);

            int[,] MatrizNum = new int[tamanho, tamanho];
            indice = 0;
            Console.WriteLine("\nMatriz em números: ");   //colocar isso em função
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
            Console.Clear();
            int[,] Chave = MatrizChave(tamanho);

            int[,] MatrizCriptografada = Criptografada(MatrizNum, Chave, tamanho, palavra);

           int Voltarousair = await Voltar();
            
            return MatrizCriptografada;
        }
        static int Determinante(int[,] matriz, int tamanho)
        {
            if (tamanho == 2)
            {
                return (matriz[0, 0] * matriz[1, 1]) - (matriz[0, 1] * matriz[1, 0]);
            }
            else // 3x3
            {
                return (matriz[0, 0] * matriz[1, 1] * matriz[2, 2] +
                        matriz[0, 1] * matriz[1, 2] * matriz[2, 0] +
                        matriz[0, 2] * matriz[1, 0] * matriz[2, 1]) -
                       (matriz[0, 2] * matriz[1, 1] * matriz[2, 0] +
                        matriz[0, 0] * matriz[1, 2] * matriz[2, 1] +
                        matriz[0, 1] * matriz[1, 0] * matriz[2, 2]);
            }
        }
        static async Task<int> Voltar()
        {
            int opcaomenu = 0;
            bool validacao = false;
            Console.WriteLine("\n1. SIM");
            Console.WriteLine("2. NÃO");
            Console.Write("Deseja voltar o Menu?: ");

            while (!validacao)
            {
                opcaomenu = ValidacaoInt();
                if(opcaomenu == 1)
                {
                   await Menu();
                }
                else if(opcaomenu == 2)
                {
                    Console.Clear();
                    Console.WriteLine("Saindo...");
                    validacao = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Digite uma opção válida: ");
                    Console.ResetColor();
                    validacao = false; //volta pra pergunta
                }
            }
            return opcaomenu;
        }
        static void Descriptografia()
        {
            Console.WriteLine("Função em andamento.");
        }
    }
}

