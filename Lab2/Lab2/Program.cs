using GraphPlottingApp;
using Lab3.Helpers.Loggers;
using Lab3.Helpers.Statistics;
using Lab3.Models;
using ScottPlot;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            double runTimes = 25;

            List<SimulationStats> stats = new List<SimulationStats>();

            for(int i = 0; i < runTimes; i++)
            {
                ILogger logger = new Logger(false);
                HavenModel haven = new(logger);
                stats.Add(haven.StartSimulation(30000));
                logger.Dispose();
                Console.WriteLine();
            }

            Console.WriteLine("**************RESULTS:************** \n");

            double avgServingTime = stats.Average(el => el.avgServingTime);

            Console.WriteLine("Avg queue size: " + stats.Average(el => el.meanQueueLength)); 
            Console.WriteLine("Avg loading: " + stats.Average(el => el.loading)); 
            Console.WriteLine("Avg woking parts: " + stats.Average(el => el.avgWorkingParts)); 
            Console.WriteLine("Min serving time: " + stats.Min(el => el.minServingTime)); 
            Console.WriteLine("Max serving time: " + stats.Max(el => el.maxServingTime)); 
            Console.WriteLine("Avg serving time: " + avgServingTime);
            int index = 0;
            foreach (var item in stats)
            {
                ++index;
                Console.WriteLine(index + " " + item.avgServingTime);
            }
            //PlotHelper plt = new PlotHelper();
            //plt.GetPlot();

            //double difSquareSum = 0;

            //stats.ForEach(el =>
            //{
            //    double dif = el.avgServingTime - avgServingTime;
            //    double difSquare = dif * dif;
            //    Console.Write("\nAvg serving time: " + el.avgServingTime);
            //    Console.Write(" Dif from avg: " + dif);
            //    Console.Write(" Dif^2 from avg: " + difSquare);
            //    difSquareSum += difSquare;
            //});
            //Console.WriteLine("\n\nAvg difSquare of " + stats.Count + " simulations: " + difSquareSum/stats.Count);
        }
    }
}