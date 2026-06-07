using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
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
                int opcao = await Menu();
                Console.Clear();

                if (opcao == 1)
                {
                    Criptografar();

                }
                else if (opcao == 2)
                {
                    Descriptografar();
                }
                else if (opcao == 3)
                {
                    ComoFunciona();
                }
                else if (opcao == 4)
                {
                    ChaveComoFunciona();
                }
                Console.WriteLine("\nDeseja voltar ao Menu?");
                Console.WriteLine("1. SIM");
                Console.WriteLine("2. NÃO");
                Console.Write("O QUE DESEJA?:");

                bool validacao = false;
                while (!validacao)
                {
                    int escolha = Validacao.ValidacaoInt();
                    if (escolha == 1)
                    {
                        validacao = true;
                    }
                    else if (escolha == 2)
                    {
                        Console.Clear();
                        Console.WriteLine("Saindo...");
                        sair = true;
                        validacao = true;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Escolha entre 1 e 4");
                    }
                }
                Console.Clear();
            }
        }
        static async Task<int> Menu()//se tem await, a função precisa ser asynk task ao invés de void
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            await Animacao("CRIPTOGRAFIA DE HILL!", 20);
            Console.ResetColor();
            Console.WriteLine("\n1. Criptografar uma frase ");
            Console.WriteLine("2. Descriptografar uma frase");
            Console.WriteLine("------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("ORIENTAÇÕES/AJUDA");
            Console.ResetColor();
            Console.WriteLine("3. Como funciona a Criptografia de HILL?");
            Console.WriteLine("4. Como escolher uma chave válida");
            Console.WriteLine("------------------------------------------");
            Console.Write("\nO QUE DESEJA?:");
            bool validacao = false;
            int escolhamenu = 0;
            while (!validacao)
            {
                escolhamenu = Validacao.ValidacaoInt();
                validacao = true;

                if (escolhamenu <= 0 || escolhamenu > 4)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("CRIPTOGRAFIA DE HILL!");
                    Console.ResetColor();
                    Console.WriteLine("\n1. Criptografar uma frase ");
                    Console.WriteLine("2. Descriptografar uma frase");
                    Console.WriteLine("------------------------------------------");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("ORIENTAÇÕES/AJUDA");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("3. Como funciona a Criptografia de HILL?");
                    Console.WriteLine("4. Como escolher uma chave válida");
                    Console.WriteLine("------------------------------------------");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\nEscolha uma opção disponível: ");
                    Console.ResetColor();
                    validacao = false;
                }
            }

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
        static void ComoFunciona()
        {
            Console.WriteLine("O método de Criptografia de Hill é um método de criptografia baseado em álgebra linear.");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nCOMO FUNCIONA:");
            Console.ResetColor();
            Console.WriteLine("\n1º Sua palavra é convertida em números (a=1, b=2, ... z=26)");
            Console.WriteLine("2º Os números são organizados em uma matriz");
            Console.WriteLine("3º Essa matriz é multiplicada por uma matriz chave");
            Console.WriteLine("4º O resultado é aplicado módulo 26, gerando a palavra criptografada");
            Console.WriteLine("Criptografar:   m' = C · m (mod 26)");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nPara descriptografar, o processo é inverso:");
            Console.ResetColor();
            Console.WriteLine("\n1º A matriz chave precisa ter inverso modular em mod 26");
            Console.WriteLine("2º A matriz inversa é calculada e multiplicada pela matriz criptografada");
            Console.WriteLine("3º O resultado é convertido de volta para letras");
            Console.WriteLine("Descriptografar: m = C⁻¹ · m' (mod 26)");
        }
        static void ChaveComoFunciona()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("COMO ESCOLHER UMA CHAVE");
            Console.ResetColor();
            Console.WriteLine("\nA chave é uma matriz NxN onde:");
            Console.WriteLine("1º Matriz 2x2: palavras até 4 letras");
            Console.WriteLine("2º Matriz 3x3: palavras até 9 letras");
            Console.WriteLine("\nPara ser válida o determinante");
            Console.WriteLine("da chave não pode ser divisível");
            Console.WriteLine("por 2 ou por 13");
            Console.WriteLine("\nExemplo válido 2x2:");
            Console.WriteLine("      3  2");
            Console.WriteLine("      5  7");
            Console.WriteLine("det = (3x7)-(2x5) = 11  ✓");
            Console.WriteLine("\nExemplo inválido 2x2:");
            Console.WriteLine("      2  4");
            Console.WriteLine("      1  3");
            Console.WriteLine("det = (2x3)-(4x1) = 2   ✗");
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
        static int[,] MatrizChave(int tamanho)
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

                int det = Determinante(Chave, tamanho);
                int invMod = InversoModular(det);

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
        static int[,] Criptografada(int[,] matrizpalavra, int[,] matrizchave, int tamanho, string verso)
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
        static int[,] Criptografar()
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
        static int[,] Submatriz(int[,] matriz, int linhaRemover, int colunaRemover)
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
        static int[,] CofatorChave(int[,] chave, int tamanho)
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
                        int[,] sub = Submatriz(chave, i, j);
                        int det = (sub[0, 0] * sub[1, 1]) - (sub[0, 1] * sub[1, 0]);
                        cofator[i, j] = (int)Math.Pow(-1, i + j) * det;
                    }
                }
            }
            return cofator;
        }
        static int InversoModular(int det)
        {
            det = ((det % 26) + 26) % 26;
            for (int x = 1; x < 26; x++)
                if ((det * x) % 26 == 1)
                    return x;
            return -1;
        }
        static void Descriptografar()
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
            int[,] chave = MatrizChave(tamanho);

            //Calcula determinante
            int det = Determinante(chave, tamanho);

            //Calcula inverso modular
            int invMod = InversoModular(det);

            //Calcula cofator
            int[,] cofator = CofatorChave(chave, tamanho);

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






