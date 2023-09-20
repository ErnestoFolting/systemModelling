using Lab1.Generators;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            IGenerator generator = new Generator1();
            IGenerator generator2 = new Generator2();
            IGenerator generator3 = new Generator3();
            generator3.Generate(100);
        }
    }
}