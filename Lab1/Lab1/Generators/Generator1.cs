namespace Lab1.Generators
{
    public class Generator1 : IGenerator
    {
        private const double _lambda = 0.5;
        public List<double> Generate(int numbersToGenerate)
        {
            List<double> generatedNumbers = new List<double>();

            Random random = new Random();
            double x = 0;

            for (int i = 0; i < numbersToGenerate; i++)
            {
                double randomNumber = random.NextDouble();
                x = (-1 / _lambda) * Math.Log(randomNumber);
                Console.WriteLine(x);
                generatedNumbers.Add(x);
            }

            return generatedNumbers;
        }
        
        public double functionValue(double x)
        {
            return 1 - Math.Pow(Math.E, x * (_lambda * (-1)));
        }
    }
}
