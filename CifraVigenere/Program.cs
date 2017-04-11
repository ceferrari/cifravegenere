using System;
using System.Diagnostics;
using System.Text;

namespace CifraVigenere
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;

            var opcao = -1;
            while (opcao != 0)
            {
                Stopwatch watch;
                Console.WriteLine("[ 1 ] Criptografar");
                Console.WriteLine("[ 2 ] Descriptografar");
                Console.WriteLine("[ 0 ] Sair do Programa");
                Console.WriteLine("----------------------------------");
                Console.Write("Escolha uma opção: ");
                Console.WriteLine();
                if (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    opcao = -1;
                }
                switch (opcao)
                {
                    case 1:
                        watch = Stopwatch.StartNew();
                        Console.WriteLine();
                        Executar(Criptografar);
                        break;
                    case 2:
                        watch = Stopwatch.StartNew();
                        Console.WriteLine();
                        Executar(Descriptografar);
                        break;
                    case 0:
                        watch = null;
                        Console.WriteLine();
                        Console.WriteLine("Pressione uma tecla para finalizar...");
                        break;
                    default:
                        watch = null;
                        Console.WriteLine();
                        Console.WriteLine("Opção inválida!");
                        break;
                }
                if (watch != null)
                {
                    watch.Stop();
                    Console.WriteLine("----------------------------------");
                    Console.WriteLine("Tempo decorrido: " + TimeSpan.FromMilliseconds(watch.ElapsedMilliseconds).ToString(@"hh\h\:mm\m\:ss\s\:fff\m\s"));
                }
                Console.ReadKey();
                Console.Clear();
            }
        }

        private static void Executar(Func<string, string, string> funcao)
        {
            Console.WriteLine("Informe o texto:");
            var texto = Console.ReadLine().ToUpper();
            Console.WriteLine();

            Console.WriteLine("Informe a chave:");
            var chave = Console.ReadLine().ToUpper();
            Console.WriteLine();

            Console.WriteLine("Resultado:");
            try
            {
                Console.WriteLine(funcao(texto, chave));
            }
            catch
            {
                Console.WriteLine("Erro! Tente novamente.");
            }
        }

        private static string Criptografar(string texto, string chave)
        {
            var resultado = "";
            for (var i = 0; i < texto.Length; i++)
            {
                var vTexto = Convert.ToInt32(texto[i]);
                var vChave = Convert.ToInt32(chave[i % chave.Length]);
                var vAscii = (vTexto + vChave) % 26 + 65;
                resultado += (char)vAscii;
            }
            return resultado;
        }

        private static string Descriptografar(string texto, string chave)
        {
            var resultado = "";
            for (var i = 0; i < texto.Length; i++)
            {
                var vTexto = Convert.ToInt32(texto[i]);
                var vChave = Convert.ToInt32(chave[i % chave.Length]);
                var vAscii = vTexto - vChave + (vTexto >= vChave ? 65 : 91);
                resultado += (char)vAscii;
            }
            return resultado;
        }
    }
}