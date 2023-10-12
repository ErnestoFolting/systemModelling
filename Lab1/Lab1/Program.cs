using Lab1.Generators;
using Lab1.Helpers;

namespace Lab1
{
    class Program
    {
        private const int typeOfGenerator = 2;
        private const int intervalsCount = 100;
        private const int runTimes = 50;
        static void Main(string[] args)
        {
            IGenerator generator;
            switch (typeOfGenerator)
            {
                case 1:
                    generator = new Generator1();
                    break;
                case 2:
                    generator = new Generator2();
                    break;
                default:
                    generator = new Generator3();
                    break;
            }

            var generatedNumbers = generator.Generate(10000);
            StatsHelper.GetStats(generatedNumbers, Convert.ToString(typeOfGenerator), intervalsCount);

            AvgConfidenceHelper.GetAvg(generator,runTimes, intervalsCount);
        }
    }
}