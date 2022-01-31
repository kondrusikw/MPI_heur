using System;
using System.Collections.Generic;
using System.Text;
using OptimizationProject.Algorithm_Folder;
using OptimizationProject.Graph_Folder;

namespace OptimizationProject.Result_Folder
{
    public class Result
    {
      public enum ResultType { DAP, DDAP }
        public DateTime TimeStamp { get; set; }
       public ResultType TypeOfResult { get; set; }
        public double ValueOfCostFunction { get; set; }
        public int NumberOfIterations { get; set; }
        public double TimeOfExecution { get; set; }
        public Chromosome Solution { get; set; }
        //public Demand demand { get; set; }
        public Graph graph { get; set; }
        public List<Chromosome> BestSolutionStack { get; set; }
        public int TimeToBest { get; set; }
        public int GenerationsToBest { get; set; }

        /*public override string ToString()
        {
            string ResultString = "Time Stamp: " + DateTime.Now.ToString() + " \nType of task: " + TypeOfResult.ToString() + " \n" +
                "Value of Cost Function: " + ValueOfCostFunction.ToString() + " \nNumber of Iterations: " + NumberOfIterations.ToString() +
                "\nTime of Execution: " + TimeOfExecution.ToString() + " ms\nTime to best: " + TimeToBest.ToString() + " ms\nGenerations to best: " + GenerationsToBest.ToString() + "\n\nBest solution stack:" + Environment.NewLine;

            foreach (Chromosome chrom in BestSolutionStack)
                ResultString += chrom.ToShortString();
            ResultString += "======================================";

            ResultString += " \n \n \n Description of chromosome: " + Solution.ToString();
            ResultString += "\n\n\nSOLUTION\n";
            ResultString += LegitResult();
            return ResultString;
        }
        public string GetFileName()
        {
            return TypeOfResult.ToString() + " " + DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH'-'mm'-'ss");
        }*/

        /*private string LegitResult()
        {
            return SolutionStr();
        }*/
        /*private string SolutionStr()
        {
            return LinkPart() + Environment.NewLine + DemandPart();
        }*/
        /*private string LinkPart()
        {
            return graph.Edges.Count + Environment.NewLine + linkLoadList();
        }

        private string linkLoadList()
        {
            string output = "";
            List<int> loads = Solution.CalculateLoads(graph, TypeOfResult);
            for(int i = 0; i < loads.Count; i++)
                loads[i] += graph.Edges[i].NumberOfModules * graph.Edges[i].SizeOfModule;
            int temp;
            if (ResultType.DAP == TypeOfResult) temp = graph.Edges[0].NumberOfModules;
            else temp = (int)Math.Ceiling((double)loads[0] / graph.Edges[0].SizeOfModule);
            output += "1 " + loads[0].ToString() + " " + Math.Max(temp, graph.Edges[0].NumberOfModules).ToString();
            for (int i = 1; i < loads.Count; i++)
            {
                output += Environment.NewLine;
                if (ResultType.DAP == TypeOfResult) temp = graph.Edges[i].NumberOfModules;
                else temp = (int)Math.Ceiling((double)loads[i] / graph.Edges[i].SizeOfModule);
                output += (i + 1).ToString() + " " + loads[i].ToString() + " " + Math.Max(temp, graph.Edges[i].NumberOfModules).ToString();
            }
            return output;
        }

        private string DemandPart()
        {
            return graph.Demands.Count.ToString() + Environment.NewLine + DemandFlowList();
        }
        private string DemandFlowList()
        {
            string output = "";
            output += DemandFlow(0);
            for (int i = 1; i < graph.Demands.Count; i++)
            {
                output += Environment.NewLine;
                output += DemandFlow(i);
            }
            return output;
        }

        private string DemandFlow(int i)
        {
            return (i + 1).ToString() + " " + graph.Demands[i].Paths.Count.ToString() + Environment.NewLine + DemandPathFlowList(Solution.ListOfGenes[i]);
        }

        private string DemandPathFlowList(Gene gene)
        {
            string output = "";
            output += DemandPathFlow(0, gene.ValueOnPath[0]);
            for (int i = 1; i < gene.ValueOnPath.Length; i++)
            {
                output += Environment.NewLine;
                output += DemandPathFlow(i, gene.ValueOnPath[i]);
            }
            return output;
        }

        private string DemandPathFlow(int i, int v)
        {
            return (i + 1).ToString() + " " + v;
        }*/
    }
}
