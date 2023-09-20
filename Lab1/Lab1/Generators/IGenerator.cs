namespace Lab1.Generators
{
    public interface IGenerator
    {
        public List<double> Generate(int numbersToGenerate);
        public double functionValue(double x);
    }
}
