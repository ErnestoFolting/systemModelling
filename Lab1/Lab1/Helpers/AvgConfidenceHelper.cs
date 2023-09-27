using Lab1.Generators;

namespace Lab1.Helpers
{
    public class AvgConfidenceHelper
    {
        public static double GetAvg(IGenerator generator, int runTimes, int intervalsCount)
        {
            double sum = 0;

            for (int i = 0; i < runTimes; i++)
            {
                var generatedNumbers = generator.Generate(10000);
                sum += DistributionChecker.CheckHypothesis(generatedNumbers, generator, intervalsCount);
            }

            double avg = Math.Round(sum / runTimes, 2);
            
            Console.WriteLine("Avg confidence: " + avg);

            return avg;
        }
    }
}
