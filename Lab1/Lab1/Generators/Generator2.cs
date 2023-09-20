namespace Lab1.Generators
{
    public class Generator2 : IGenerator
    {
        private double _a = 0;
        private double _sigma = 1;

        public List<double> Generate(int numbersToGenerate)
        {
            List<double> generatedNumbers = new List<double>();

            Random random = new Random();
            double x = 0;

            for (int i = 0; i < numbersToGenerate; i++)
            {

                double sum = 0;
                for (int j = 0; j < 12; j++)
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
            //return 1 / (_sigma * Math.Sqrt(2 * Math.PI)) * Math.Exp(-(Math.Pow((x - _a), 2) / 2 * Math.Pow(_sigma, 2)));
            return 1 / 2 * (1 + ErfHelper.Erf((x - _a) / (_sigma * Math.Sqrt(2))));
        }
    }
}
