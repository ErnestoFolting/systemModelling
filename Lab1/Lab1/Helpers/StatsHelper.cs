using ScottPlot;
using ScottPlot.Statistics;

namespace Lab1.Helpers
{
    public class StatsHelper
    {
        public static void GetStats(List<double> generatedNumbers, string nameOfFile, int intervalsCount)
        {

            double avg = generatedNumbers.Average();

            Console.WriteLine("Average: " + avg);

            double sumOfDifferences = 0;
            foreach (double number in generatedNumbers)
            {
                sumOfDifferences += Math.Pow(number - avg, 2);
            }

            double dispersion = sumOfDifferences / generatedNumbers.Count;

            Console.WriteLine("Dispersion: " + dispersion);

            Plot plt = new Plot(1500, 1000);
            Histogram hist = new(generatedNumbers.Min(), generatedNumbers.Max(), intervalsCount);

            hist.AddRange(generatedNumbers);

            var bar = plt.AddBar(values: hist.Counts, positions: hist.Bins);
            bar.BarWidth = (generatedNumbers.Max() - generatedNumbers.Min()) / hist.BinCount;

            plt.SaveFig(nameOfFile + ".jpg");
        }
    }
}
