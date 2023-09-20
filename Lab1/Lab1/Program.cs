using Lab1.Generators;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            IGenerator generator = new Generator1();
            generator.Generate(10);
            //Console.WriteLine(generator.functionValue(5)); 
        }
    }
}