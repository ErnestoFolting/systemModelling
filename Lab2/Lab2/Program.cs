using Lab3.Helpers.Loggers;
using Lab3.Models;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            ILogger logger = new Logger(true);
            HavenModel haven = new(logger);
            haven.StartSimulation();
        }
    }
}