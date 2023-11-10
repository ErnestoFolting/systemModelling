using Lab3.Models;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            HavenModel haven = new();
            haven.StartSimulation();
        }
    }
}