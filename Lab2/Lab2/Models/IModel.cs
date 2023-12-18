using Lab3.Helpers.Statistics;

namespace Lab3.Models
{
    public interface IModel
    {
        public SimulationStats StartSimulation(int time);
    }
}
