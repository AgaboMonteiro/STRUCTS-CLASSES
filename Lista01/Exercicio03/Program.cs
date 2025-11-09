using System;
using System.ComponentModel;
using System.ComponentModel.Design;
namespace CadastroEletro
{

    class Program
    {
      //
        static void addEletro(List<Eletro> listaEletros) //addEletro (nome da função) List<Eletro> listaEletro (é o que a função recebe para poder funcionar.)
        {
          //tipo   nome vairavel  folha em branco
            Eletro novoEletro = new Eletro(); // Aqui você está criando um novo eletrodoméstico, uma “folha em branco” para preencher. eletro é o tipo, novoEletro é uma variavel
            Console.Write("Entre com o nome: ");
            novoEletro.nome = Console.ReadLine();

            Console.Write("Entre com a potencia: ");
            novoEletro.potencia = double.Parse(Console.ReadLine());

            Console.Write("Entre com o tempo medio de uso diário: ");
            novoEletro.tempoMedioUsoDiario = double.Parse(Console.ReadLine());

          //adiciona dentro da lista a variavel criada acima
            listaEletros.Add(novoEletro);
        }



        static void mostrarEletros(List<Eletro> listaEletros)
        {
            int posicao = 1;
            foreach (Eletro e in listaEletros)// foreach para cada e-(pode ser qualquer nome), 
            {
                Console.WriteLine($"**** ELETRO {posicao} ****");
                Console.WriteLine($"Nome: {e.nome}");
                Console.WriteLine($"Potência: {e.potencia}");
                Console.WriteLine($"Tempo Médio: {e.tempoMedioUsoDiario}");
                Console.WriteLine();
                    //usa o 'e' aqui
                posicao++;
            }
        }

         static void salvarDados(List<Eletro> listaEletros, string nomeArquivo)
        {

            using (StreamWriter writer = new StreamWriter(nomeArquivo))
            {
                foreach (Eletro e in listaEletros)
                {
                    writer.WriteLine($"{e.nome};{e.potencia};{e.tempoMedioUsoDiario}"); //split por virgula por causa daqui
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
            Console.WriteLine("3 - Buscar Eletros com nome");
            Console.WriteLine("4 - Buscar Eletros com potencias maior que X");
            Console.WriteLine("5 - Consumo Diário");
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
                    string[] campos = linha.Split(';'); //split por virgula
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

        static bool buscarEletro(List<Eletro> listaEletros, string nomeBusca)
        {
            foreach (Eletro e in listaEletros) //pra cada 'e' do tipo eletro (tem que dar um nome para variavel do tipo e)
            {
                if (e.nome.ToUpper().Contains(nomeBusca.ToUpper())) // pega a variavel 'e' coloca em maiuscula e verifica se Conains a variavel nomebusca transformada em maiuscula
                {
                    Console.WriteLine("***Eletrodoméstico***");
                    Console.WriteLine($"Nome: {e.nome}");
                    Console.WriteLine($"Potência: {e.potencia}");
                    Console.WriteLine($"Tempo Médio: {e.tempoMedioUsoDiario}");
                    Console.WriteLine();
                    return true; //colcoar fora do foreach, não colcoar else return false

                }//fim do if
            }// fim do foreach
            return false;
        }



        static bool buscarPotencia(List<Eletro> listaEletros, double potenciaBusca)
        {
            bool encontrou = false;// cria uma flag q sempe fica abaixada
            foreach (Eletro e in listaEletros) //pra cada 'e' do tipo eletro (tem que dar um nome para variavel do tipo e)
            {
                if (e.potencia >= potenciaBusca) // pega a variavel 'e' coloca em maiuscula e verifica se Conains a variavel nomebusca transformada em maiuscula
                {
                    Console.WriteLine("***Eletrodoméstico***");
                    Console.WriteLine($"Nome: {e.nome}");
                    Console.WriteLine($"Potência: {e.potencia}");
                    Console.WriteLine($"Tempo Médio: {e.tempoMedioUsoDiario}");
                    Console.WriteLine();
                    encontrou = true; // se encontraar ele levanta a bandeira;
                }//fim do if
            }// fim do foreach

            return encontrou; //se não encontrou retorna falso e se encontrou retorna veredadeiro;
        }
        


        static double consumoTotalKwDia(List<Eletro> listaEletros, double valorKw)
        {
            double total = 0;
            foreach (Eletro e in listaEletros) //pra cada 'e' do tipo eletro (tem que dar um nome para variavel do tipo e)
            {

                total += e.potencia * e.tempoMedioUsoDiario;
            }
            return total;//retorna o total no dia            
        }
        


        static void Main()
        {
          //cria uma lista do tipo eletro com nome listaEletros e recebe uma nova lista
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
                    case 3:
                        Console.Write("Eletrodomestico que deseja buscar: ");
                        string nomeEletro = Console.ReadLine();
                        bool encontrou = buscarEletro(listaEletros, nomeEletro);
                        if (!encontrou)
                        {
                            Console.WriteLine("Eletrodomestico não encontrado");
                        }
                        break;
                    case 4:
                        Console.WriteLine("Potencia que deseja encontrar: ");
                        double potenciaBusca = double.Parse(Console.ReadLine());
                        bool encontrado = buscarPotencia(listaEletros,potenciaBusca); // uma variavel boll encotrado contendo 
                        if(!encontrado)//se é diferente de true
                        {
                            Console.WriteLine("Não existe eletros com essa potencia :");
                        }
                        break;
                    case 5:
                        Console.WriteLine("Valor do KW/h em R$: ");
                        double valorKw = double.Parse(Console.ReadLine());
                        double consumokw = consumoTotalKwDia(listaEletros,valorKw);
                        Console.WriteLine($"Consumo DIÁRIO em Kw: {consumokw:F2}");
                        Console.WriteLine($"Consumo DIÁRIO em R$:{(valorKw * consumokw):F2}");

                        Console.WriteLine($"Consumo MENSAL em Kw: {consumokw*30}");
                        Console.WriteLine($"Consumo MENSAL em R$:{((valorKw * consumokw)*30):F2}");                        
                        break;
                    case 0:
                        Console.WriteLine("Saindo...");
                        Console.WriteLine("Até mais");
                        salvarDados(listaEletros, "eletros.txt");
                        break;
                }
            } while (opcao != 0);

        }
    }
}


