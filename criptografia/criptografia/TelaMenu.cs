using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace criptografia
{
    internal class TelaMenu
    {
        public static async Task<int> Menu()//se tem await, a função precisa ser asynk task ao invés de void
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
        public static async Task Animacao(string texto, int velocidade)
        {
            foreach (char letra in texto)
            {
                Console.Write(letra);
                await Task.Delay(velocidade);
            }
            Console.WriteLine();
        }
        public static void ComoFunciona()
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
        public static void ChaveComoFunciona()
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
    }
}
