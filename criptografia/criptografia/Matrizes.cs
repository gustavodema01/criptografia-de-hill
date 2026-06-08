using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace criptografia
{
    internal class Matrizes
    {
        public static int Tamanhomatrizes(int qtd) //função para calcular o tamanho da matriz apartir do indice da frase
        {
            if (qtd <= 4) return 2;
            if (qtd <= 9) return 3;
            return 0; // inválido

        }
        public static string CompletarMatriz(string palavra, int tamanho)
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
        public static char[,] MatrizLetras(int largura, char[,] Matriz, string todapalavra, int indice)
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
        public static int[,] MatrizChave(int tamanho)
        {
            int[,] Chave = new int[tamanho, tamanho];
            bool chaveValida = false;

            while (!chaveValida)
            {
                Console.WriteLine("Digite a Matriz Chave: ");
                for (int i = 0; i < tamanho; i++)
                    for (int j = 0; j < tamanho; j++)
                    {
                        Console.Write($"[{i}],[{j}]:");
                        Chave[i, j] = Validacao.ValidacaoInt();
                    }

                int det = Operações.Determinante(Chave, tamanho);
                int invMod = Operações.InversoModular(det);

                if (invMod == -1)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Chave inválida! Determinante não tem inverso mod 26. Tente outra.");
                    Console.ResetColor();
                    Console.WriteLine("");
                }
                else
                {
                    chaveValida = true;
                }
            }
            Console.Clear();
            Console.WriteLine("Sua Matriz Chave:");
            for (int i = 0; i < tamanho; i++)
            {
                for (int j = 0; j < tamanho; j++)
                    Console.Write($"\t{Chave[i, j]}");
                Console.WriteLine("");
            }
            Console.WriteLine("\nOBS: NÃO ESQUEÇA A MATRIZ CHAVE.");

            return Chave;
        }
        public static int[,] Submatriz(int[,] matriz, int linhaRemover, int colunaRemover)
        {
            int[,] sub = new int[2, 2];
            int linhaAtual = 0;

            for (int i = 0; i < 3; i++)
            {
                if (i == linhaRemover) continue;
                int colunaAtual = 0;
                for (int j = 0; j < 3; j++)
                {
                    if (j == colunaRemover) continue;
                    sub[linhaAtual, colunaAtual] = matriz[i, j];
                    colunaAtual++;
                }
                linhaAtual++;
            }
            return sub;
        }


    }
}
