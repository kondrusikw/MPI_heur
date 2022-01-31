using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OptimizationProject.Graph_Folder;
using static OptimizationProject.Result_Folder.Result;

namespace OptimizationProject.Algorithm_Folder
{
    public class Chromosome
    {
        public List<Gene> ListOfGenes;
        public double GainValue;

        public Chromosome(Chromosome chromosome)//konstruktor kopiujący trszeba dorobić
        {
            GainValue = chromosome.GainValue;
            ListOfGenes = new List<Gene>();
            foreach (Gene gene in chromosome.ListOfGenes)
                ListOfGenes.Add(new Gene(gene));
        }

        public Chromosome(Graph graph, Random random)//tutaj nie wiem czy nie lepiej dać wygenerowany wcześniej losowo nowy seed, i tworzyć lokalną klasę random, bo nie wiem  czy jak jest tak jak teraz, to każdy "chromosom" nie będzie taki sam
        {
            ListOfGenes = new List<Gene>();
            bool all_services_used = false;
            bool temp_service_used = false;
           /* foreach (Demand d in graph.Demands)
            {
                int[] waitingPaths = new int[d.Paths.Count];//sprawdzi, czy inicjują się zera!!!!
                for (int i = d.Volume; i > 0; i--)//generacja losowych wartości
                    waitingPaths[random.Next(0, waitingPaths.Length - 1)]++;
                ListOfGenes.Add(new Gene(waitingPaths));//uczepienie
            }*/
            while(!all_services_used){                                  //LOSOWANIE GENES TAK DŁUGO AŻ WSZYSTKIE USŁUGI ZOSTANĄ UŻYTE
                foreach (Server s in graph.Servers)
                {
                    int[] services = new int[graph.Services.Count];     //sprawdzi, czy inicjują się zera!!!!
                    for (int i = 0; i < graph.Services.Count; i++)          //generacja losowych wartości   TUTAJ PÓKI CO ZAŁOŻYŁAM ŻE DLA JEDNEOGO SERWERA NIE MOŻE BYĆ WIĘCEJ USŁUG NIŻ JEST ICH RODZAJÓW
                        services[i] = random.Next(2);               //1 
                    ListOfGenes.Add(new Gene(services));            //uczepienie
                }

                for (int i = 0; i < graph.Services.Count; i++){
                    temp_service_used = false;
                    for (int j = 0; j < graph.Servers.Count; j++){
                        if(ListOfGenes[j].services[i] == 1){   //JESLI USLUGA JEST NA PRZYNAJMNIEJ JEDNYM SERWERZE TO PRZECHODZIMY DO KOLEJNEJ USŁUGI
                            temp_service_used = true;
                            break;
                        }
                            
                    }

                    if (temp_service_used == false){  //JEŚLI BRAKUJE DANEJ USŁUGI TO BREAK I LOSOWANIE OD POCZĄTKU
                        all_services_used = false;
                        break;
                    }

                    else
                        all_services_used = true;
                }
            }



            CalculateGainValue(graph);
        }
        public void CalculateGainValue(Graph graph)
        {
            GainValue = 0;
            //List<int> loads = CalculateLoads(graph);
            
                GainValue = SumEdgeCosts(graph/*, loads*/);

        }

        private double SumEdgeCosts(Graph graph)
        {
            int y = 0;
            int cores_sum = 0;
            int server_sum;
            double sum = 0;
            double transit_sum = 0;
            double min_path = 0;
            double temp_path = 0;
           /*for(int i=0; i<graph.Edges.Count;i++)
            {
                if (loads[i] <= 0)
                    continue;
                else
                {
                    y = (int)Math.Ceiling((double)loads[i] / graph.Edges[i].SizeOfModule);
                    sum += y * graph.Edges[i].CostOfModule;
                }
            }  
            return sum;*/
            for(int i = 0; i < ListOfGenes.Count; i++){         // ZLICZAM ZASOBY
                server_sum  = 0;
                for(int j = 0; j < graph.Services.Count; j++){
                    if(ListOfGenes[i].services[j]==1){
                        server_sum += graph.Services[j].cores;
                    }
                }

                if (server_sum > graph.Servers[i].cores){
                    cores_sum = 3000;     //JEŚLI NIE STARCZA ZASOBÓW NA SERWERZE TO SZTUCZNIE ZWIĘKSZAMY KOSZTA, TAK ŻEBY TEN CHROMOSOM NIE PRZESZEDŁ DO KOLEJNEJ POPULACJI
                    break;
                }

                else{
                    cores_sum += server_sum;
                }
            }

            cores_sum *= 10;

            for (int i = 0; i < graph.Users.Count; i++){
                temp_path = 0;
                min_path = 0;
                if(ListOfGenes[graph.Users[i].server_id].services[graph.Users[i].service_id] == 0){  //JEŚLI NA SERWERZE PRZY KTÓRYM JEST UŻYTKOWNIK NIE MA JEGO USŁUGI TO LICZEMY KOSZT DROGI
                    for (int j = 0; j < ListOfGenes.Count; j++){
                        if(ListOfGenes[j].services[graph.Users[i].service_id]==1){
                           temp_path = Math.Pow(graph.Servers[j].x -graph.Servers[graph.Users[i].server_id].x,2)+Math.Pow(graph.Servers[j].y -graph.Servers[graph.Users[i].server_id].y,2);
                           if (temp_path < min_path || min_path == 0)
                                min_path = temp_path;
                        }
                    }

                    transit_sum += min_path;
                }
            }

            sum = cores_sum + transit_sum;
            return sum;
        }

        /*public List<int> CalculateLoads(Graph graph)
        {
            List<int> loads = InitializeList(graph.Edges.Count); //loads[0] obciążenie na krawędzi index 1 loads[1] na krawędzi index 2 itd...

            for(int i=0; i<graph.Demands.Count;i++)
            {
                for (int j=0;j<graph.Demands[i].Paths.Count;j++)
                {
                    for (int k=0; k<graph.Edges.Count;k++)
                    {
                        if (graph.Demands[i].Paths[j].CheckIfContainEdge(graph.Edges[k]))
                        {
                            loads[graph.Edges[k].Id - 1] += ListOfGenes[i].ValueOnPath[j];
                        }

                    }
                }
            }
            for (int i = 0; i < loads.Count; i++)
                loads[i] -= graph.Edges[i].Capacity;
            return loads;
        }*/
        /*public List<int> InitializeList(int Length)
        {
            List<int> InitializedList = new List<int>();
            for (int i = 0; i < Length; i++)
                InitializedList.Add(0);
            return InitializedList;
        }*/
        public int mutate(Random random, double ProbabilityMutation, Graph graph)
        {
            bool temp_service_used = false;
            int NoMutations = 0;
            foreach (Gene gene in ListOfGenes)
            {
                if (random.NextDouble() < ProbabilityMutation)
                {
                    gene.mutate(random);
                    NoMutations++;
                }
            }

            foreach (Gene gene in ListOfGenes) {
                for (int i = 0; i < graph.Services.Count; i++){
                    temp_service_used = false;
                    for (int j = 0; j < graph.Servers.Count; j++){
                        if(ListOfGenes[j].services[i] == 1){   //JESLI USLUGA JEST NA PRZYNAJMNIEJ JEDNYM SERWERZE TO PRZECHODZIMY DO KOLEJNEJ USŁUGI
                            temp_service_used = true;
                            break;
                        }                            
                    }

                    if (temp_service_used == false){  //JEŚLI BRAKUJE DANEJ USŁUGI TO LOSUJEMY DOWOLNY GENE I DODAJEMY DO NIEGO TĄ USŁUGĘ
                        int k = random.Next(0, graph.Servers.Count);
                        ListOfGenes[k].services[i] = 1;
                    }
            } 
            }
            CalculateGainValue(graph);//nowy zmutowany walju
            return NoMutations;
        }
        /*public Chromosome cross(Random random, Chromosome second, Graph graph)
        {
            Chromosome temp = new Chromosome(this);
            for (int i = 0; i < ListOfGenes.Count; i++)
                if (random.Next(0, 1) == 1) temp.ListOfGenes[i] = new Gene(second.ListOfGenes[i]);
            temp.CalculateGainValue(graph);
            return temp;
        }*/

        /*public override string ToString()
        {
            string Result = "Chromosome: \n";
            int i = 1;
            foreach(var gene in ListOfGenes)
            {
                Result+="Gene for demand number: "+i.ToString() + "\n";
                foreach (var number in gene.ValueOnPath)
                {
                    Result += number.ToString() + " ";
                }
                Result += "\n *********** \n";
                ++i;
            }
            return Result;
        }*/
        public string ToShortString()
        {
            string Result = "Chromosome value: " + GainValue + "\n";
            return Result;
        }
    }
}
