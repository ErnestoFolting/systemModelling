using Lab1.Generators;
using MathNet.Numerics.Distributions;

namespace Lab1
{
    public class DistributionChecker
    {
        public static double CheckHypothesis(List<double> generatedNumbers, IGenerator generator, int intervalsCount)
        {
            double min = generatedNumbers.Min();
            double max = generatedNumbers.Max();

            //Differences between limits
            double intervalSize = (max - min) / intervalsCount;

            int[] numbersInIntervalCount = new int[intervalsCount];
            generatedNumbers.ForEach(el =>
            {
                int index = (int)((el - min) / intervalSize);
                numbersInIntervalCount[el == max ? index - 1 : index]++;
            });

            //Merge intervals where n < 5
            List<int> newCountsInIntervals = new List<int>();

            //Pairs of new interval limits
            List<(int LeftLimit, int RightLimit)> newIntervalLimits = new List<(int LeftLimit, int RightLimit)>();

            int currentNumbersCount = 0;
            int leftIndex = 0;

            for (int i = 0; i < intervalsCount; i++)
            {
                currentNumbersCount += numbersInIntervalCount[i];

                if (currentNumbersCount >= 5)
                {
                    newCountsInIntervals.Add(currentNumbersCount);
                    newIntervalLimits.Add((leftIndex, i + 1));
                    leftIndex = i + 1;
                    currentNumbersCount = 0;
                }
            }

            //Push remained numbers to the last interval
            if (currentNumbersCount != 0)
            {
                newCountsInIntervals[newCountsInIntervals.Count - 1] += currentNumbersCount;
                newIntervalLimits.Add((leftIndex, numbersInIntervalCount.Length));
            }

           
            //Calculation of x2
            double x2 = 0;
            for (int i = 0; i < newCountsInIntervals.Count; i++)
            {
                double left = min + intervalSize * newIntervalLimits[i].LeftLimit;
                double leftLimitFunctionValue = generator.functionValue(left);

                double right = min + intervalSize * newIntervalLimits[i].RightLimit;
                double rightLimitFunctionValue = generator.functionValue(right);

                double numberInIntervalChance = rightLimitFunctionValue - leftLimitFunctionValue;

                double expectedCount = generatedNumbers.Count * numberInIntervalChance;

                x2 += Math.Pow(newCountsInIntervals[i] - expectedCount, 2) / expectedCount;
            }

            int freedomDegrees = newCountsInIntervals.Count - 2;

            ChiSquared chiSquared = new ChiSquared(freedomDegrees);

            double tableX2 = 0;
            double confidenceСhance = 0;

            for (double significanceLevel = 0.01; significanceLevel <= 1; significanceLevel += 0.01)
            {
                tableX2 = chiSquared.InverseCumulativeDistribution(significanceLevel);
                if (x2 < tableX2)
                {
                    confidenceСhance = 1.0 - significanceLevel;
                    break;
                }
            }

            confidenceСhance = Math.Round(confidenceСhance, 2);
            Console.WriteLine("Degrees of freedom: " + freedomDegrees);
            Console.WriteLine("Real X2 " + x2);
            Console.WriteLine("Expected X2 " + tableX2 + " Confidence " + confidenceСhance);

            return confidenceСhance;
        }
    }
}
