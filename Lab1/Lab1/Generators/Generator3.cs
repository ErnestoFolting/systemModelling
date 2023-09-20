using System.Reflection.Metadata.Ecma335;

namespace Lab1.Generators
{
    public class Generator3 : IGenerator
    {
        private double _a = Math.Pow(5, 13);
        private double _c = Math.Pow(2, 31);
        public List<double> Generate(int numbersToGenerate)
        {
            List<double> generatedNumbers = new List<double>();

            double z = 1;

            double x;
            for (int i = 0; i < numbersToGenerate; i++)
            {
                z = (_a * z) % _c;
                x = z / _c;
                Console.WriteLine(x);
            }
            
            return generatedNumbers;
        }

        public double functionValue(double x)
        {
            throw new NotImplementedException();
        }
    }
}
