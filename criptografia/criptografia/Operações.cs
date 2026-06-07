using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace criptografia
{
    internal class Operações
    {
        public static int[,] Criptografada(int[,] matrizpalavra, int[,] matrizchave, int tamanho, string verso)
        {
            Console.Clear();
            int[,] resultado = new int[tamanho, tamanho];

            for (int i = 0; i < tamanho; i++)
                for (int j = 0; j < tamanho; j++)
                {
                    resultado[i, j] = 0;
                    for (int k = 0; k < tamanho; k++)
                        resultado[i, j] += matrizchave[i, k] * matrizpalavra[k, j];
                    resultado[i, j] = resultado[i, j] % 26;
                }

            Console.Write("Palavra: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(verso.ToUpper());
            Console.ResetColor();
            Console.WriteLine(" Criptografada em números:");

            for (int i = 0; i < tamanho; i++)
            {
                for (int j = 0; j < tamanho; j++)
                    Console.Write($"\t{resultado[i, j]}");
                Console.WriteLine("");
            }

            Console.Write("\nPalavra criptografada em letras: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            for (int i = 0; i < tamanho; i++)
                for (int j = 0; j < tamanho; j++)
                {
                    if (resultado[i, j] == 0)
                        Console.Write("*");
                    else
                        Console.Write((char)(resultado[i, j] + 'a' - 1));
                }
            Console.ResetColor();
            Console.WriteLine();

            return resultado;
        }
        public static int[,] Criptografar()
        {
            Console.Write("Digite a palavra(até 9 letras): ");
            string palavra = Validacao.ValidacaoLetras(); //chama a função Validacao para garantir que escreva letras

            int indice = 0;
            Console.WriteLine("\nTabela ASCII");
            foreach (char letra in palavra) //pega um índice de palavra e atribui a letra
            {
                indice = char.ToLower(letra) - 'a' + 1; // 'a' vale 97 na tabela ASCII. Então: letra - 97 + 1. ex: 'a'(97) - 'a'(97) = 0 + 1 = 1
                Console.WriteLine($"{char.ToUpper(letra)} = {indice}"); //essa letra é esse número
            }
            int tamanho = Matrizes.Tamanhomatrizes(palavra.Length); //dimensão da matriz de acordo com a quantidade de letras em palavra. exp =3x3 = 3, 4x4 = 4...

            string palavraCompleta = Matrizes.CompletarMatriz(palavra, tamanho); //palavra com os *, se necessário

            char[,] Matriz = new char[tamanho, tamanho];
            Console.Clear();

            indice = 0;
            Console.WriteLine("Matriz em letras: ");
            char[,] letras = Matrizes.MatrizLetras(tamanho, Matriz, palavraCompleta, indice);

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
            int[,] Chave = Matrizes.MatrizChave(tamanho);

            int[,] MatrizCriptografada = Operações.Criptografada(MatrizNum, Chave, tamanho, palavra);

            return MatrizCriptografada;
        }
        public static int Determinante(int[,] matriz, int tamanho)
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
        public static int[,] CofatorChave(int[,] chave, int tamanho)
        {
            int[,] cofator = new int[tamanho, tamanho];

            if (tamanho == 2)
            {
                cofator[0, 0] = chave[1, 1];
                cofator[0, 1] = -chave[0, 1];
                cofator[1, 0] = -chave[1, 0];
                cofator[1, 1] = chave[0, 0];
            }
            else // 3x3
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        int[,] sub = Matrizes.Submatriz(chave, i, j);
                        int det = (sub[0, 0] * sub[1, 1]) - (sub[0, 1] * sub[1, 0]);
                        cofator[i, j] = (int)Math.Pow(-1, i + j) * det;
                    }
                }
            }
            return cofator;
        }
        public static int InversoModular(int det)
        {
            det = ((det % 26) + 26) % 26;
            for (int x = 1; x < 26; x++)
                if ((det * x) % 26 == 1)
                    return x;
            return -1;
        }
        public static void Descriptografar()
        {
            int tamanho = 0;
            bool validacao = false;
            Console.Write("Digite o tamanho da matriz (2 ou 3): ");
            while (!validacao)
            {
                tamanho = Validacao.ValidacaoInt();
                if (tamanho == 2 || tamanho == 3)
                {
                    validacao = true; // sai do while
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Digite apenas 2 ou 3: ");
                    Console.ResetColor();
                }
            }
            Console.Clear();
            int[,] matrizCript = new int[tamanho, tamanho]; // 1. Pede a matriz criptografada
            Console.WriteLine("Digite a matriz criptografada:");
            for (int i = 0; i < tamanho; i++)
                for (int j = 0; j < tamanho; j++)
                {
                    Console.Write($"[{i},{j}]: ");
                    matrizCript[i, j] = Validacao.ValidacaoInt();
                }

            //Pede a matriz chave
            int[,] chave = Matrizes.MatrizChave(tamanho);

            //Calcula determinante
            int det = Operações.Determinante(chave, tamanho);

            //Calcula inverso modular
            int invMod = Operações.InversoModular(det);

            //Calcula cofator
            int[,] cofator = Operações.CofatorChave(chave, tamanho);

            //Monta matriz inversa
            int[,] inversa = new int[tamanho, tamanho];
            for (int i = 0; i < tamanho; i++)
                for (int j = 0; j < tamanho; j++)
                {
                    inversa[j, i] = ((invMod * cofator[i, j]) % 26 + 26) % 26;
                }

            //Multiplica inversa × matrizCript
            int[,] resultado = new int[tamanho, tamanho];
            for (int i = 0; i < tamanho; i++)
                for (int j = 0; j < tamanho; j++)
                {
                    resultado[i, j] = 0;
                    for (int k = 0; k < tamanho; k++)
                        resultado[i, j] += inversa[i, k] * matrizCript[k, j];
                    resultado[i, j] = ((resultado[i, j] % 26) + 26) % 26;
                }

            //Converte números para letras
            Console.Write("\nPalavra descriptografada: ");
            for (int i = 0; i < tamanho; i++)
                for (int j = 0; j < tamanho; j++)
                {
                    if (resultado[i, j] == 0) break; // era *
                    char letra = (char)(resultado[i, j] + 'a' - 1);
                    Console.Write(char.ToUpper(letra));
                }
            Console.WriteLine();
        }




    }
}
