using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using OptimizationProject.Graph_Folder;
using OptimizationProject.Result_Folder;
using static OptimizationProject.ProgramRunner;
using static OptimizationProject.Result_Folder.Result;

namespace OptimizationProject.Algorithm_Folder
{
    class EAlgorithm
    {
        private Graph graph;
        //private StopCondition Condition;
        private int ConditionValue;
        //private double ProbabilityCrossOver;
        private double ProbabilityMutation;
        private int TargetPopulation;
        
        Random random;
        private List<Chromosome> CurrentResultsTable;
        private List<Chromosome> TemporaryResultsTable;
        private List<Chromosome> BestSolutionStack;


        //warunki stopu
        private int Time; // czas wykonywania algorytmu
        private int NoMutations; //liczba mutacji
        private int NoGenerations;// liczba generacji
        private int NoBetterSolutions;//Ilość iteracji bez poprawy
        private double bestSolutionValue;

        private int TimeToBest;
        private int GenerationsToBest;

        //ResultType resultType;
        public EAlgorithm(Graph graph, int startingPopulation, double probabilityMutation, int timeGeneratorSeed)
        {
            this.graph = graph;
            //Condition = condition;
            //ProbabilityCrossOver = probabilityCrossOver;
            ProbabilityMutation = probabilityMutation;
            TargetPopulation = startingPopulation;
            random = new Random(timeGeneratorSeed);
            ConditionValue = 20;                            //(condition == StopCondition.Time) ? conditionValue * 1000 : conditionValue;
            //this.resultType = resultType;

            CurrentResultsTable = new List<Chromosome>();
            TemporaryResultsTable = new List<Chromosome>();
            for (int i = 0; i < startingPopulation; i++)
                CurrentResultsTable.Add(new Chromosome(graph, random));

            Time = 0;
            NoMutations = 0;
            NoGenerations = 0;
            NoBetterSolutions = 0;
            CurrentResultsTable.Sort((x, y) => x.GainValue.CompareTo(y.GainValue));
            bestSolutionValue = CurrentResultsTable[0].GainValue;
            BestSolutionStack = new List<Chromosome>();
            BestSolutionStack.Add(new Chromosome(CurrentResultsTable[0]));
            TimeToBest = 0;
            GenerationsToBest = 0;
        }

        public void Run()
        {
            //Result result = new Result();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while(CheckStopCondition())
            {
                //Cross();//do populacji zostają dodane z jakiś P zcrossowane dwa rozwiązania

                Mutate();//populacja jest kopiowana i mutowana (cała)  TODO 

                Clean();//wbierane jest N najlepszych rozwiązan (N=starting population?)

                Time = (int)stopwatch.ElapsedMilliseconds;

                CheckNewSolution();//aktualna populacja jest sprawdzana - jesli najlepsze rozwiazanie sie zmienilo jest dopisywane do stosu (NoBetterSolutions - 0), jesli nie yo zwiekszana jest wartość NoBetterSolutions
            }
            stopwatch.Stop();

            int j = 0;
            foreach (Gene gene in BestSolutionStack[0].ListOfGenes){
                Console.Write("Server ");  
                Console.Write(j);
                Console.Write(": | "); 
                for(int i = 0; i < gene.services.Length; i++){                
                Console.Write(gene.services[i]);   
                Console.Write(" |");
                }
                j++;
                Console.WriteLine("");
            }

            Console.Write("result: ");
            Console.WriteLine(bestSolutionValue);
            //SetResult(result);
            //return result;
        }
        /*public void SetResult(Result result, ResultType resultType)
        {
            result.Solution = CurrentResultsTable[0];
            result.ValueOfCostFunction = bestSolutionValue;
            result.TypeOfResult = resultType;
            result.NumberOfIterations = NoGenerations;
            result.TimeOfExecution = Time;
            result.BestSolutionStack = BestSolutionStack;
            result.TimeToBest = TimeToBest;
            result.GenerationsToBest = GenerationsToBest;
            result.graph = graph;
        }*/
        private bool CheckStopCondition()
        {
            /*switch(Condition)
            {
                case StopCondition.Time:
                    if (Time > ConditionValue) return false;
                    break;
                case StopCondition.LackOfBetterSolution:
                    if (NoBetterSolutions > ConditionValue) return false;
                    break;
                case StopCondition.NoGenerations:
                    if (NoGenerations > ConditionValue) return false;
                    break;
                case StopCondition.NoMutations:
                    if (NoMutations > ConditionValue) return false;
                    break;
            }
            return true;*/

            if (NoBetterSolutions > ConditionValue)
                return false;
            else
                return true;
        }
        private void Mutate()//zakłądam, że każdy gen mutuje, a prawdopodobieństwo mutacji dotyczy nie tego czy chromosom zmutuje, ale czy każdy gen z osobna.
        {
            TemporaryResultsTable = new List<Chromosome>();
            foreach (Chromosome chromosome in CurrentResultsTable)
            {
                Chromosome uberChromosome = new Chromosome(chromosome);
                NoMutations += uberChromosome.mutate(random, ProbabilityMutation, graph);
                TemporaryResultsTable.Add(uberChromosome);
            }
        }
        /*private void Cross()//jeśli zajdzie cross z P=zadane P, to losujemy z jakimś P ważonym po jakości, które się krzyżują i krzyżujemy
        {
            Shuffle(ref CurrentResultsTable);
            int loops = CurrentResultsTable.Count / 2;
            for (int i = 0; i < loops; i++)
                if(random.NextDouble() < ProbabilityCrossOver)
                {
                    CurrentResultsTable.Add(CurrentResultsTable[2 * i].cross(random, CurrentResultsTable[2 * i + 1], graph, resultType));
                }

        }*/
        private void Clean()//wybieramy TOP ileś najlepszych reszta do utylizacji
        {
            CurrentResultsTable.AddRange(TemporaryResultsTable);
            CurrentResultsTable.Sort((x, y) => x.GainValue.CompareTo(y.GainValue));
            CurrentResultsTable.RemoveRange(TargetPopulation, CurrentResultsTable.Count - TargetPopulation);
            ++NoGenerations; // generacja to nowa populacja więc jak usuwamy to tworzymy nową populację imo dlatego +1
        }
        private void CheckNewSolution()//sprawdzamy czy wynik lepszy, jeśli tak, to dodajemy do Resultsów, do stosu postępu, jeśli nie, to dodajemy do mziennej kolejny nieudany eksperyment na ludziach
        {
            if (bestSolutionValue <= CurrentResultsTable[0].GainValue) NoBetterSolutions++;
            else
            {
                TimeToBest = Time;
                GenerationsToBest = NoGenerations;
                NoBetterSolutions = 0;
                bestSolutionValue = CurrentResultsTable[0].GainValue;
                BestSolutionStack.Add(new Chromosome(CurrentResultsTable[0]));
            }
        }
        public void Shuffle(ref List<Chromosome> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                Chromosome value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
