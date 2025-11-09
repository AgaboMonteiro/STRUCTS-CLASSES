using System.Collections;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace CadastroLivro
{
    class Program
    {



        static int menu()
        {
            Console.WriteLine("--------------");
            Console.WriteLine("Sistema de CADASTRO DE LIVROS");
            Console.WriteLine("1 - Adicionar um livro");
            Console.WriteLine("2 - Mostrar Livros");
            Console.WriteLine("3 - Procure um livro por título");
            Console.WriteLine("4 - Todos os livros mais novos que o ano digitado");
            Console.WriteLine("0 - Salvar dados e Sair do sistema");
            int opcao = int.Parse(Console.ReadLine());
            return opcao;
        }



        static void carregarDados (List<Livro> listaLivro, string nomeArquivo)//um metodo carregar dados que recebe listaEletros e o nome do arquivo
        {
            if (File.Exists(nomeArquivo))
            {
                string[] linhas = File.ReadAllLines(nomeArquivo);
                foreach (string linha in linhas)
                {
                    string[] campos = linha.Split(',');
                    Livro novoLivro = new Livro();
                    novoLivro.titulo = campos[0];
                    novoLivro.autor = campos[1];
                    novoLivro.ano = int.Parse(campos[2]);
                    novoLivro.prateleira = int.Parse(campos[3]);
                    listaLivro.Add(novoLivro);
                }
                Console.WriteLine("Dados carregados com sucesso!");
            }
            else
                Console.WriteLine("Arquivo não encontrado :(");
        }



        static void mostrarLivros(List<Livro> listaLivros)
        {
            int posicao = 1;
            foreach (Livro l in listaLivros)// para cada variavel l do tipo Livro na lista livros
            {
                Console.WriteLine($"**** LIVRO {posicao} ****");
                Console.WriteLine($"Titulo: {l.titulo}");
                Console.WriteLine($"Autor: {l.autor}");
                Console.WriteLine($"Ano: {l.ano}");
                Console.WriteLine($"Prateleira: {l.prateleira}");
                Console.WriteLine();
                posicao++;
            }
        }



        static bool buscarAno(List<Livro> listaLivros, int anoDigitado)
        {
            int posicao = 1;
            bool encontrou = false;
            foreach (Livro l in listaLivros)
            {
                if (l.ano > anoDigitado)
                {
                    Console.WriteLine($"**** LIVRO {posicao} ****");
                    Console.WriteLine($"Titulo: {l.titulo}");
                    Console.WriteLine($"Autor: {l.autor}");
                    Console.WriteLine($"Ano: {l.ano}");
                    Console.WriteLine($"Prateleira: {l.prateleira}");
                    Console.WriteLine();
                    encontrou =  true;
                }//fim if
                posicao++;
            }//fim do foreach
            return encontrou;
        }



        static bool buscarLivro(List<Livro> listaLivros, string nomeTitulo)
        {
            int posicao = 1;
            foreach (Livro l in listaLivros)
            {
                if (l.titulo.ToUpper().Contains(nomeTitulo.ToUpper()))
                {
                    Console.WriteLine($"**** LIVRO {posicao} ****");
                    Console.WriteLine($"Titulo: {l.titulo}");
                    Console.WriteLine($"Autor: {l.autor}");
                    Console.WriteLine($"Ano: {l.ano}");
                    Console.WriteLine($"Prateleira: {l.prateleira}");
                    Console.WriteLine();
                    return true;
                }//fim if
                posicao++;
            }//fim do foreach
            return false;
        }





        static void addLivro(List<Livro> listaLivros) //
        {
            Livro novoLivro = new Livro();
            Console.WriteLine("Entre com o título: ");
            novoLivro.titulo = Console.ReadLine();

            Console.WriteLine("Entre com o autor: ");
            novoLivro.autor = Console.ReadLine();

            Console.WriteLine("Entre com o ano: ");
            novoLivro.ano = int.Parse(Console.ReadLine());

            Console.WriteLine("Entre com a prateleira: ");
            novoLivro.prateleira = int.Parse(Console.ReadLine());

            listaLivros.Add(novoLivro);

        }



        static void salvarDados(List<Livro> listaLivros, string nomeArquivo)
        {

            using (StreamWriter writer = new StreamWriter(nomeArquivo))
            {
                foreach (Livro l in listaLivros)
                {
                    writer.WriteLine($"{l.titulo},{l.autor},{l.ano},{l.prateleira}");
                }
            }
            Console.WriteLine("Dados salvos com sucesso");
        }



        static void Main()//parentes indica que main é um metodo e não uma variavel ou classe, e parenteses servem para passar parametros que o metodo pode receber
        {
            List<Livro> listaLivros = new List<Livro>(); //crio uma lista do tipo livro que vai se chamar listalivros e recebe uma lista novinha de livros sem nada ainda
            int opcao = 0;
            carregarDados(listaLivros, "livros.txt");

            do
            {
                opcao = menu();
                switch(opcao)
                {
                    case 1:
                        addLivro(listaLivros);
                        break;

                    case 2:
                        mostrarLivros(listaLivros);
                        break;

                    case 3:
                        Console.Write("Qual tıtulo deseja buscar: ");
                        string nomeTitulo = Console.ReadLine();
                        bool achou = buscarLivro(listaLivros, nomeTitulo);
                        if (!achou)
                        {
                            Console.WriteLine("Livro não encontrado");
                        }
                        break;

                    case 4:
                        Console.Write("Digite um ano: ");
                        int anoDigitado = int.Parse(Console.ReadLine());
                        bool encontrou = buscarAno(listaLivros, anoDigitado);
                        if (!encontrou)
                        {
                            Console.WriteLine("Livro não encontrado");
                        }
                        break;

                    case 0:
                        Console.WriteLine("Saindo...");
                        Console.WriteLine("Até mais");
                        salvarDados(listaLivros, "livros.txt");
                        break;
                    }

                } while (opcao != 0) ;

            }
    }
}