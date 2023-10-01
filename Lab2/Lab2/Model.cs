using Lab2.Elements;

namespace Lab2
{
    public class Model
    {
        private List<Element> elements = new();
        public double timeNext;
        public double timeCurrent;
        int nextEventId;

        public Model(List<Element> elements)
        {
            this.elements = elements;
            timeNext = 0;
            nextEventId = 0;
            timeCurrent = timeNext;
        }

        public void Simulation(double timeOfSimulation)
        {
            while(timeCurrent < timeOfSimulation) {
                timeNext = Double.MaxValue;
                foreach(Element element in elements)
                {
                    if (element.timeNext < timeNext)
                    {
                        timeNext = element.timeNext;
                        nextEventId = element.elementId;
                    }
                }

                Element? nextElement = elements.Find(el => el.elementId == nextEventId);

                Console.WriteLine("\n\n\nNext will be event in " + nextElement?.elementName + " , time of this event = " + timeNext);
                foreach(Element element in elements)
                {
                    element.EvaluateStats(timeNext - timeCurrent);
                }

                timeCurrent = timeNext;

                elements.ForEach(el => el.timeCurrent = timeCurrent);

                //nextElement.Exit();

                elements.ForEach(el =>
                {
                    if(el.timeNext == timeCurrent) el.Exit();
                });
                Console.WriteLine("...Current time: " + timeCurrent);
                PrintCurrentStats();
            }
            PrintResult();
        }

        private void PrintCurrentStats()
        {
            elements.ForEach(el => el.PrintCurrentStat());
        }

        private void PrintResult()
        {
            Console.WriteLine("\n********************************Results********************************");
            elements.ForEach(el =>
            {
                el.PrintStat();
                if (el.GetType() == typeof(ProcessElement))
                {
                    ProcessElement p = (ProcessElement)el;
                    Console.WriteLine("Mean queue length: " + p.meanQueueSize / timeCurrent + 
                        "\nFailure probability: " + p.failureElements / (double)p.exitedElements + 
                        "\nLoading " + p.timeInWork / timeCurrent);
                }
            });
        }
    }
}
