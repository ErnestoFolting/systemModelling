using Lab3.Elements;

namespace Lab3.Helpers.StatsOutput
{
    public class HospitalStatsOutputHelper : IStatsOutputHelper
    {
        public List<Element> elements;

        public HospitalStatsOutputHelper(List<Element> element)
        {
            this.elements = element;
        }

        public void GetStats()
        {
            Console.WriteLine("\n\n\nHospital Stats");
        }
    }
}
