using Lab3.Elements;

namespace Lab3.Helpers.StatsOutput
{
    public class BankStatsOutputHelper : IStatsOutputHelper
    {
        public List<Element> elements;

        public BankStatsOutputHelper(List<Element> element)
        {
            this.elements = element;
        }

        public void GetStats()
        {
            Console.WriteLine("\n\n\nBank Stats");
            Console.WriteLine("Queue changes: " + ProcessElement.queueChanges);
        }
    }
}
