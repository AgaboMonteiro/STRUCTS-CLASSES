using System;
using System.IO;

namespace ControleJogos
{
    class Emprestimo
    {
        public string Data { get; set; }
        public string NomePessoa { get; set; }
        public bool Emprestado { get; set; }
    }

    class Jogo
    {
        public string Titulo { get; set; }
        public string Console { get; set; }
        public int Ano { get; set; }
        public double Ranking { get; set; }
        public Emprestimo InfoEmprestimo { get; set; }
    }

    class Program
    {
        static void carregarDados(Jogo[] jogos, string nomeArquivo)
        {
            if (File.Exists(nomeArquivo))
            {
                string[] linhas = File.ReadAllLines(nomeArquivo);
                int limite = Math.Min(linhas.Length, jogos.Length);

                for (int i = 0; i < limite; i++)
                {
                    string[] campos = linhas[i].Split(';');
                    Jogo j = new Jogo();
                    j.Titulo = campos[0];
                    j.Console = campos[1];
                    j.Ano = int.Parse(campos[2]);
                    j.Ranking = double.Parse(campos[3]);

                    j.InfoEmprestimo = new Emprestimo();
                    j.InfoEmprestimo.Emprestado = (campos[4].ToUpper() == "S");
                    j.InfoEmprestimo.Data = campos[5];
                    j.InfoEmprestimo.NomePessoa = campos[6];

                    jogos[i] = j;
                }
                Console.WriteLine("✅ Dados carregados com sucesso!");
            }
            else
            {
                Console.WriteLine("⚠️ Arquivo não encontrado, será criado ao salvar.");
            }
        }

        static void salvarDados(Jogo[] jogos, string nomeArquivo)
        {
            using (StreamWriter w = new StreamWriter(nomeArquivo))
            {
                for (int i = 0; i < jogos.Length; i++)
                {
                    if (jogos[i] != null)
                    {
                        Jogo j = jogos[i];
                        w.WriteLine($"{j.Titulo};{j.Console};{j.Ano};{j.Ranking};{(j.InfoEmprestimo.Emprestado ? "S" : "N")};{j.InfoEmprestimo.Data};{j.InfoEmprestimo.NomePessoa}");
                    }
                }
            }
            Console.WriteLine("💾 Dados salvos com sucesso!");
        }

        static int procurarJogo(Jogo[] jogos, string titulo)
        {
            for (int i = 0; i < jogos.Length; i++)
            {
                if (jogos[i] != null && jogos[i].Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase))
                    return i;
            }
            return -1;
        }

        static void listarPorConsole(Jogo[] jogos, string nomeConsole)
        {
            bool achou = false;
            for (int i = 0; i < jogos.Length; i++)
            {
                if (jogos[i] != null && jogos[i].Console.Equals(nomeConsole, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Título: {jogos[i].Titulo} | Ano: {jogos[i].Ano} | Ranking: {jogos[i].Ranking}");
                    achou = true;
                }
            }
            if (!achou) Console.WriteLine("Nenhum jogo encontrado para esse console.");
        }

        static void listarEmprestados(Jogo[] jogos)
        {
            Console.WriteLine("\n=== JOGOS EMPRESTADOS ===");
            bool achou = false;

            for (int i = 0; i < jogos.Length; i++)
            {
                if (jogos[i] != null && jogos[i].InfoEmprestimo.Emprestado)
                {
                    Console.WriteLine($"Título: {jogos[i].Titulo} | Para: {jogos[i].InfoEmprestimo.NomePessoa} | Data: {jogos[i].InfoEmprestimo.Data}");
                    achou = true;
                }
            }

            if (!achou)
                Console.WriteLine("Nenhum jogo emprestado no momento.");
        }

        static int menu()
        {
            Console.WriteLine("\n----------- MENU -----------");
            Console.WriteLine("1 - Procurar jogo por título");
            Console.WriteLine("2 - Listar todos os jogos de um console");
            Console.WriteLine("3 - Realizar empréstimo de um jogo");
            Console.WriteLine("4 - Devolver jogo");
            Console.WriteLine("5 - Mostrar jogos emprestados");
            Console.WriteLine("0 - Salvar e sair");
            Console.Write("Escolha: ");
            return int.Parse(Console.ReadLine());
        }

        static void Main()
        {
            Console.Write("Quantos jogos deseja cadastrar (máx)? ");
            int n = int.Parse(Console.ReadLine());

            Jogo[] jogos = new Jogo[n];
            carregarDados(jogos, "jogos.txt");

            int opcao;
            do
            {
                opcao = menu();

                switch (opcao)
                {
                    case 1:
                        Console.Write("Digite o título do jogo: ");
                        string titulo = Console.ReadLine();
                        int pos = procurarJogo(jogos, titulo);
                        if (pos >= 0)
                        {
                            Jogo j = jogos[pos];
                            Console.WriteLine($"Título: {j.Titulo}");
                            Console.WriteLine($"Console: {j.Console}");
                            Console.WriteLine($"Ano: {j.Ano}");
                            Console.WriteLine($"Ranking: {j.Ranking}");
                            Console.WriteLine($"Emprestado: {(j.InfoEmprestimo.Emprestado ? "Sim" : "Não")}");
                        }
                        else Console.WriteLine("Jogo não encontrado!");
                        break;

                    case 2:
                        Console.Write("Digite o nome do console: ");
                        string nomeConsole = Console.ReadLine();
                        listarPorConsole(jogos, nomeConsole);
                        break;

                    case 3:
                        Console.Write("Digite o título do jogo a emprestar: ");
                        titulo = Console.ReadLine();
                        pos = procurarJogo(jogos, titulo);
                        if (pos >= 0 && !jogos[pos].InfoEmprestimo.Emprestado)
                        {
                            Console.Write("Nome da pessoa: ");
                            jogos[pos].InfoEmprestimo.NomePessoa = Console.ReadLine();
                            jogos[pos].InfoEmprestimo.Data = DateTime.Now.ToString("dd/MM/yyyy");
                            jogos[pos].InfoEmprestimo.Emprestado = true;
                            Console.WriteLine("📦 Empréstimo registrado com sucesso!");
                        }
                        else Console.WriteLine("Jogo não encontrado ou já emprestado!");
                        break;

                    case 4:
                        Console.Write("Digite o título do jogo a devolver: ");
                        titulo = Console.ReadLine();
                        pos = procurarJogo(jogos, titulo);
                        if (pos >= 0 && jogos[pos].InfoEmprestimo.Emprestado)
                        {
                            jogos[pos].InfoEmprestimo.Emprestado = false;
                            jogos[pos].InfoEmprestimo.NomePessoa = "-";
                            jogos[pos].InfoEmprestimo.Data = "-";
                            Console.WriteLine("✅ Jogo devolvido com sucesso!");
                        }
                        else Console.WriteLine("Jogo não encontrado ou não emprestado!");
                        break;

                    case 5:
                        listarEmprestados(jogos);
                        break;

                    case 0:
                        salvarDados(jogos, "jogos.txt");
                        Console.WriteLine("👋 Saindo do sistema...");
                        break;

                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }
            } while (opcao != 0);
        }
    }
}
