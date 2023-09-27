using Lab1.Helpers;

namespace Lab1.Generators
{
    public class Generator2 : IGenerator //Normal
    {
        private double _a = 2; //math expectation
        private double _sigma = 0.5; //стандартне відхилення

        public List<double> Generate(int numbersToGenerate)
        {
            List<double> generatedNumbers = new();

            Random random = new Random();

            double x = 0;

            for (int i = 0; i < numbersToGenerate; i++)
            {
                double sum = 0;
                for (int j = 1; j <= 12; j++)
                {
                    sum += random.NextDouble();
                }
                double u = sum - 6;
                x = _sigma * u + _a;

                generatedNumbers.Add(x);
            }

            return generatedNumbers;
        }

        public double functionValue(double x)
        {
            return (1 + ErfHelper.Erf((x - _a) / (Math.Sqrt(2) * _sigma))) / 2;
        }
    }
}
