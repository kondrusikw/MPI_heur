using System;
using System.Collections.Generic;
using System.Text;
using OptimizationProject.Algorithm_Folder;
using OptimizationProject.Graph_Folder;
//using OptimizationProject.Parser_Folder;
//using OptimizationProject.Result_Folder;

namespace OptimizationProject
{
    class ProgramRunner
    {
        public enum StopCondition { Time, NoGenerations, NoMutations, LackOfBetterSolution};

       // private Parser Pars { get; set; }
        private AlgorithmRunner Algorithm { get; set; }
        private int StartingPopulation = 10;//{ get; set; }
       // private double ProbabilityCrossOver { get; set; }
        private double ProbabilityOfMutation = 0.5; // { get; set; }
        private int TimeGeneratorSeed { get; set; }
        //private int NumberOfCases { get; set; } // liczba grafów
       // private StopCondition Condition { get; set; }

        private List<Server> servers;
        private List<Service> services;
        private List<User> users;

       // private int ConditionValue { get; set; }

        public ProgramRunner()
        {
            //Pars = new Parser();
            Algorithm = new AlgorithmRunner();
            servers = new List<Server> {new Server(20, 7, 8), new Server(12, 4, 2), new Server(12,5,2), new Server(12,6,2), new Server(12,7,2) };
            services = new List<Service> {new Service(5), new Service(4), new Service(4), new Service(4), new Service(2) };
            users = new List<User> {new User(0,0,1), new User(0,1,1), new User(1,1,1), new User(1,4,1), new User(2,3,1),new User(3,0,1),new User(3,3,2),new User(4,2,1),new User(4,4,1) };

           // NumberOfCases = Pars.Files.Count;

        }

        public void Run()
        {
            
            Console.WriteLine("OAST - Projekt 2!\n Wpisz swój super wybór");
            int Choice = 0;
            while(true)
            {
                Console.WriteLine("1. Wykonaj optymalizację");
                Console.WriteLine("2. Zakończ proces");
                Choice = Convert.ToInt32(Console.ReadLine());
                if(Choice != 1 && Choice !=2)
                {
                    Console.WriteLine("Złą opcja");
                    continue;
                }
                else if(Choice==2)
                {
                    Console.WriteLine("Zamykanie programu");
                    break;
                }
                else
                {
                    //FillDataToAlgorithm();

                    Graph graph = new Graph (servers, users, services);         //Pars.ReadConfigFiles(FileChooser()); //TODO DANE

                    Algorithm.RunAlgorithm(graph, StartingPopulation, ProbabilityOfMutation, TimeGeneratorSeed);


                    //Pars.WriteResultToFile(Result);

                    //Result DDAPResult=Algorithm.RunAlgorithm(graph, StartingPopulation, ProbabilityCrossOver, ProbabilityOfMutation, TimeGeneratorSeed, Condition, ConditionValue, Result.ResultType.DDAP);
                   // Pars.WriteResultToFile(DDAPResult);
                }
            }
            
        }

       /* public void FillDataToAlgorithm()
        {
            Console.WriteLine("Konfiguracja:");
            Console.WriteLine("Wpisz liczebnosć początkowej populacji");

            StartingPopulation = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Wpisz prawdopodobieństwo krzyżowania");

            ProbabilityCrossOver = Convert.ToDouble(Console.ReadLine()); // powinno się wyjątek dać tutać no ale cóż
            Console.WriteLine("Wpisz prawdopodobieństwo mutacji");
            ProbabilityOfMutation = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Wpisz ziarno dla generatora liczb");
            TimeGeneratorSeed = Convert.ToInt32(Console.ReadLine());
            ChoiceOfStopCondition();
        }*/

        /*public void ChoiceOfStopCondition()
        {
            Console.WriteLine("Wybierz warunek stopu:");
            ShowListOfPossibleStopConditions();
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Condition = StopCondition.Time;
                    break;
                case 2:
                    Condition = StopCondition.NoGenerations;
                    break;
                case 3:
                    Condition = StopCondition.NoMutations;
                    break;
                case 4:
                    Condition = StopCondition.LackOfBetterSolution;
                    break;
                default:
                    Console.WriteLine("wybierz jeszcze raz");
                    ChoiceOfStopCondition();
                    break;
            }
            ChooseConditionValue(); // wybór wartości dla warunku stopu
        }*/
        /*public void ShowListOfPossibleStopConditions()
        {
            Console.WriteLine("1.Time Condition [s]");
            Console.WriteLine("2.Number of Generations");
            Console.WriteLine("3.Number of Mutations");
            Console.WriteLine("4.No Better solution");
        }*/

        /*private int FileChooser()
        {
            int theChoosenOne = 0;
            while(theChoosenOne <= 0 || theChoosenOne > Pars.Files.Count)
            {
                Console.WriteLine("Wybierz sieć do optymalizacji?");
                Pars.PrintFiles();
                try
                {
                    theChoosenOne = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Błąd - wybierz jeszcze raz");
                    theChoosenOne = 0;
                }
            }
            return theChoosenOne - 1;
        }*/
       /* public void ChooseConditionValue()
        {
            Console.WriteLine("Wpisz wartość dla warunku stopu");
            ConditionValue= Convert.ToInt32(Console.ReadLine());
        }*/
    }
}
