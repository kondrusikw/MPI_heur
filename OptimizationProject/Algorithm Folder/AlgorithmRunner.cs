using System;
using System.Collections.Generic;
using System.Text;
using OptimizationProject.Graph_Folder;
using OptimizationProject.Result_Folder;
using static OptimizationProject.ProgramRunner;
using static OptimizationProject.Result_Folder.Result;

namespace OptimizationProject.Algorithm_Folder
{
    class AlgorithmRunner
    {
        public void RunAlgorithm (Graph graph,int StartingPopulation,/* double ProbabilityCrossOver,*/ double ProbabilityMutation, int TimeGeneratorSeed) /*StopCondition Condition, int ConditionValue, ResultType resultType*/
        {
                EAlgorithm algorithm = new EAlgorithm(graph,/* Condition,*/ StartingPopulation, /*ProbabilityCrossOver,*/ ProbabilityMutation, TimeGeneratorSeed/*, ConditionValue, resultType*/);
                algorithm.Run();
        }
    }
}
