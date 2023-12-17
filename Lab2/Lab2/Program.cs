using Lab3.Helpers.Loggers;
using Lab3.Helpers.Statistics;
using Lab3.Models;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            double runTimes = 10;

            List<SimulationStats> stats = new List<SimulationStats>();

            for(int i = 0; i < runTimes; i++)
            {
                ILogger logger = new Logger(false);
                HavenModel haven = new(logger);
                logger.Dispose();
                stats.Add(haven.StartSimulation());
                Console.WriteLine();
            }

            Console.WriteLine("**************RESULTS:************** \n");
            Console.WriteLine("Avg queue size: " + stats.Average(el => el.meanQueueLength)); 
            Console.WriteLine("Avg failure probability: " + stats.Average(el => el.failureProbability)); 
            Console.WriteLine("Avg loading: " + stats.Average(el => el.loading)); 
            Console.WriteLine("Min serving time: " + stats.Min(el => el.minServingTime)); 
            Console.WriteLine("Max serving time: " + stats.Max(el => el.maxServingTime)); 
            Console.WriteLine("Avg serving time: " + stats.Average(el => el.avgServingTime)); 
        }
    }
}