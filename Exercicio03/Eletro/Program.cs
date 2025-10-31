using System;
using System.ComponentModel;
using System.ComponentModel.Design;
namespace CadastroEletro
{

    class Program
    {

        static void addEletro(List<Eletro> listaEletros) //addEletro (nome da função) List<Eletro> listaEletro (é o que a função recebe para poder funcionar.)
        {
            Eletro novoEletro = new Eletro(); // Aqui você está criando um novo eletrodoméstico, uma “folha em branco” para preencher. eletro é o tipo, novoEletro é uma variavel
            Console.Write("Entre com o nome: ");
            novoEletro.nome = Console.ReadLine();

            Console.Write("Entre com a potencia: ");
            novoEletro.potencia = double.Parse(Console.ReadLine());

            Console.Write("Entre com o tempo medio de uso diário: ");
            novoEletro.tempoMedioUsoDiario = double.Parse(Console.ReadLine());

            listaEletros.Add(novoEletro);
        }



        static void mostrarEletros(List<Eletro> listaEletros)
        {
            int posicao = 1;
            foreach (Eletro e in listaEletros)// foreach para cada e-(pode ser qualquer nome), 
            {
                Console.WriteLine($"**** ELETRO {posicao}****");
                Console.WriteLine($"{e.nome} - {e.potencia} - {e.tempoMedioUsoDiario}");//usa o e aqui
                posicao++;
            }
        }

         static void salvarDados(List<Eletro> listaEletros, string nomeArquivo)
        {

            using (StreamWriter writer = new StreamWriter(nomeArquivo))
            {
                foreach (Eletro e in listaEletros)
                {
                    writer.WriteLine($"{e.nome},{e.potencia},{e.tempoMedioUsoDiario}"); //split por virgula por causa daqui
                }
            }
            Console.WriteLine("Dados salvos com sucesso!");
        }



        static int menu()
        {
            Console.WriteLine("-----------");
            Console.WriteLine("*** Sistema de cadastro de Eletros ***");
            Console.WriteLine("1 - Adicionar Eletro");
            Console.WriteLine("2 - Mostrar Eletros");
            Console.WriteLine("0 - Salvar dados e Sair do sistema");
            int opcao = int.Parse(Console.ReadLine());
            return opcao;
        }



        static void carregarDados(List<Eletro> listaEletros, string nomeArquivo)
        {
            if (File.Exists(nomeArquivo))
            {
                string[] linhas = File.ReadAllLines(nomeArquivo);
                foreach (string linha in linhas)
                {
                    string[] campos = linha.Split(','); //split por virgula
                    Eletro novoEletro = new Eletro();
                    novoEletro.nome = campos[0];
                    novoEletro.potencia = double.Parse(campos[1]);
                    novoEletro.tempoMedioUsoDiario = double.Parse(campos[2]);
                    listaEletros.Add(novoEletro);
                }
                Console.WriteLine("Dados carregados com sucesso!");
            }
            else
                Console.WriteLine("Arquivo não encontrado :(");
        }
        



        static void Main()
        {
            List<Eletro> listaEletros = new List<Eletro>();//vou criar uma lista do tipo eletro e vai se chamar listaEletro que recebe uma lista novinha de eletros sem nada ainda 
            int opcao = 0;
            carregarDados(listaEletros,"eletros.txt");

            do
            {
                opcao = menu();
                switch (opcao)
                {
                    case 1:
                        addEletro(listaEletros); //Criando uma nova fichinha de um eletro
                        break;
                    case 2:
                        mostrarEletros(listaEletros);
                        break;
                    case 0:
                        Console.WriteLine("Até mais;");
                        salvarDados(listaEletros, "eletros.txt");
                        break;


                }
            } while (opcao != 0);

        }
    }
}


