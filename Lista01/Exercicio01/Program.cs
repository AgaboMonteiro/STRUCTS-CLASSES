using System;
using System.Collections.Generic;
using System.IO;


namespace CadastroBandas //tem q estar dentro do msm pacote program e Banda
{
    class Program
    {



        static void addBanda(List<Banda> listaBandas) //função para adicionr a banda na lista
        {
            Banda novaBanda = new Banda(); //precisa new Banda() sendo tipo uma cópia da banda folha branca = recebe a cópia de banda(o new serve para criar uma nova banda)
            Console.Write("Nome: ");
            novaBanda.nome = Console.ReadLine();
            Console.Write("Genero: ");
            novaBanda.genero = Console.ReadLine();
            Console.Write("Integrantes: ");
            novaBanda.integrantes = int.Parse(Console.ReadLine());
            Console.Write("Ranking: ");
            novaBanda.ranking = int.Parse(Console.ReadLine());

            listaBandas.Add(novaBanda);//adiciona a nova banda na lista, pega o papel preenchido e guarda
        }



        static void mostrarBandas(List<Banda> listaBandas)
        {
            int posicao = 1;
            foreach (Banda b in listaBandas)// foreach para cada b-(pode ser qualquer nome), 
            {
                Console.WriteLine($"**** Banda {posicao}****");
                Console.WriteLine($"{b.nome} - {b.genero} - {b.integrantes} - {b.ranking}");//usa o b aqui
                posicao++;
            }
        }



        static bool buscarBanda(List<Banda> listaBandas, string nomeBusca) //receber a lista do tipo banda chamado listaBandas
        {
            int posicao = 1;
            foreach(Banda b in listaBandas) // para cada elemento que existe dentro da minha estrutura (qual o tipo de elemnto q tenho em cada posição do vetor uma banda ou seja um b para cada banda b)
            {
                if(b.nome.ToUpper().Contains(nomeBusca.ToUpper())) // comparar string não é recomendado usar == pois pode dar alguns erros, comparar as duas maiusculas Equals ou Contains
                //pode usar o Contains para pode digitar apenas algumas letras q a banda contem
                {
                    Console.WriteLine($"*** Dados da banda***");
                    Console.WriteLine($"Nome: {b.nome}");
                    Console.WriteLine($"Genero: {b.genero}");
                    Console.WriteLine($"Integrantes: {b.integrantes}");
                    Console.WriteLine($"Ranking: {b.ranking}");
                    return true;
                }

            }//fim foreach
            return false;
        }



        //para atualizar e remover precisa achar o indice
        static int buscarIndiceBanda(List<Banda> listaBandas, string nomeBusca) //receber a lista do tipo banda chamado listaBandas
        {
            
            for (int i = 0; i<listaBandas.Count; i++) // count para contar quantas bandas tem
            {
                if(listaBandas[i].nome.ToUpper().Contains(nomeBusca.ToUpper())) // compara o nome que está sendo achado
                {   
                    return i;
                }
            }//fim foreach
            return -1;
        }



        static bool atualizarBanda(List<Banda> listaBandas, string nomeBanda)//receber list do tipo banda com nome listaBandas
        {
            int i = buscarIndiceBanda(listaBandas, nomeBanda); //recebe a função buscarIndiceBanda e passa como parametro listaBandas e nmoeBanda
            if (i == -1)
                return false;
            
            Console.WriteLine("***Dados da Banda***");
            Console.WriteLine($"Nome: {listaBandas[i].nome}");
            Console.WriteLine($"Genero: {listaBandas[i].genero}");
            Console.WriteLine($"Integrantes: {listaBandas[i].integrantes}");
            Console.WriteLine($"Ranking: {listaBandas[i].ranking}");

            Console.WriteLine($"*** NOVOS DADOS**");
            Console.WriteLine($"Nome: ");
            listaBandas[i].nome = Console.ReadLine();
            Console.WriteLine($"Genero: ");
            listaBandas[i].genero = Console.ReadLine();
            Console.WriteLine($"Integrantes: ");
            listaBandas[i].integrantes = int.Parse(Console.ReadLine());
            Console.WriteLine($"Ranking: ");
            listaBandas[i].ranking = int.Parse(Console.ReadLine());
            return true;            
        }



        static bool excluirBanda(List<Banda> listaBandas, string nomeBanda)//receber list do tipo banda com nome listaBandas
        {
            int i = buscarIndiceBanda(listaBandas, nomeBanda); //recebe a função buscarIndiceBanda e passa como parametro listaBandas e nmoeBanda
            if (i == -1)
                return false;
            
            Console.WriteLine("***Dados da Banda***");
            Console.WriteLine($"Nome: {listaBandas[i].nome}");
            Console.WriteLine($"Genero: {listaBandas[i].genero}");
            Console.WriteLine($"Integrantes: {listaBandas[i].integrantes}");
            Console.WriteLine($"Ranking: {listaBandas[i].ranking}");

            Console.WriteLine($"Deseja realmente excluir os dados dessa banda? 1 - SIM | 0 - NÃO");
            int resposta = int.Parse(Console.ReadLine());

            if (resposta == 1)
            {
            Console.WriteLine($"Banda Excluida");
            listaBandas.RemoveAt(i);
            }

            return true;            
        }

        

        static int menu()
        {
            Console.WriteLine("-----------");
            Console.WriteLine("*** Sistema de cadastro de bandas ***");
            Console.WriteLine("1 - Adicionar Banda");
            Console.WriteLine("2 - Mostrar Banda");
            Console.WriteLine("3 - Buscar Banda");
            Console.WriteLine("4 - Atualizar Banda");
            Console.WriteLine("5 - Excluir Banda");
            Console.WriteLine("0 - Salvar dados e Sair do sistema");
            int opcao = int.Parse(Console.ReadLine());
            return opcao;
        }



        static void salvarDados(List<Banda> listaBandas, string nomeArquivo)
        {

            using (StreamWriter writer = new StreamWriter(nomeArquivo))
            {
                foreach (Banda b in listaBandas)
                {
                    writer.WriteLine($"{b.nome},{b.genero},{b.integrantes},{b.ranking}"); //split por virgula por causa daqui
                }
            }
            Console.WriteLine("Dados salvos com sucesso!");
        }



        



        static void Main()
        {
            List<Banda> listaBandas = new List<Banda>(); //<Banda> serve para informar qual o tipo, o new banda tira um xerox e coloca para ser preenchido, o list se organiza quando é removido de alguma posição 
            int opcao = 0; // List - um vetor q facilita organização <Banda> tipo que foi criado no Banda.cs (add nome genero integrantes e ranking)
            carregarDados(listaBandas,"bandas.txt");
            do
            {

                opcao = menu();

                switch (opcao)
                {
                    case 1:
                        addBanda(listaBandas); //criando uma nova fichinha de uma banda
                        break;
                    case 2:
                        mostrarBandas(listaBandas);
                        break;
                     case 3:
                        Console.WriteLine("Nome da Banda");
                        string nomeBanda = Console.ReadLine();
                        bool encontrado = buscarBanda(listaBandas,nomeBanda); // uma variavel boll encotrado contendo 
                        if(!encontrado)//se é diferente de true
                        {
                            Console.WriteLine("Banda não encontrada :(");
                        }
                        break;
                        
                    case 4:
                        Console.WriteLine("Nome da Banda");
                        nomeBanda = Console.ReadLine();
                        encontrado = atualizarBanda(listaBandas,nomeBanda); // uma variavel boll encotrado contendo 
                        if(!encontrado)//se é diferente de true
                        {
                            Console.WriteLine("Banda não encontrada :(");
                        }
                        break;
                    case 5:
                        Console.WriteLine("Nome da Banda");
                        nomeBanda = Console.ReadLine();
                        encontrado = excluirBanda(listaBandas,nomeBanda); // uma variavel boll encotrado contendo 
                        if(!encontrado)//se é diferente de true
                        {
                            Console.WriteLine("Banda não encontrada :(");
                        }
                        break;
                    case 0:
                        Console.WriteLine("Até mais ;)");
                        salvarDados(listaBandas, "bandas.txt");
                        break;
                }
                Console.ReadKey(); //pausa
                Console.Clear();//limpa a tela

            } while (opcao != 0);
        }
    }
}
