using Lab3.Elements;
using Lab3.Helpers.Loggers;

namespace Lab3.Models
{
    public class Model
    {
        private List<Element> elements = new();
        public double timeNext;
        public double timeCurrent;
        int nextEventId;
        public ILogger logger;
        public Model(List<Element> elements, ILogger logger)
        {
            this.elements = elements;
            timeNext = 0;
            nextEventId = 0;
            timeCurrent = timeNext;
            this.logger = logger;
        }

        public void Simulation(double timeOfSimulation, Action<double>? actionPerIteration)
        {
            while (timeCurrent < timeOfSimulation)
            {
                timeNext = double.MaxValue;
                foreach (Element element in elements)
                {
                    if (element.timeNext < timeNext)
                    {
                        timeNext = element.timeNext;
                        nextEventId = element.elementId;
                    }
                }

                double timeDelta = timeNext - timeCurrent;

                if (actionPerIteration != null) actionPerIteration(timeDelta);

                Element? nextElement = elements.Find(el => el.elementId == nextEventId);

                logger.Log("\n\n\nNext will be event in " + nextElement?.elementName + " , time of this event = " + timeNext);
                foreach (Element element in elements)
                {
                    element.EvaluateStats(timeDelta);
                }

                timeCurrent = timeNext;

                elements.ForEach(el => el.timeCurrent = timeCurrent);

                elements.ForEach(el =>
                {
                    if (el.timeNext == timeCurrent) el.Exit();
                });
                logger.Log("...Current time: " + timeCurrent);
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
            logger.Log("\n********************************Results********************************");
            elements.ForEach(el =>
            {
                el.PrintStat();
                if (el.GetType() == typeof(ProcessElement))
                {
                    ProcessElement p = (ProcessElement)el;
                    logger.Log("\n\n" + p.elementName + ":");
                    logger.Log("Mean queue length: " + p.meanQueueSize / timeCurrent +
                        "\nFailure probability: " + p.failureElements / (double)(p.exitedElements + p.failureElements) +
                        "\nLoading " + p.timeInWork / timeCurrent +
                        "\nMin serving time " + p.servingTimeStats.minServingTime+
                        "\nMax serving time " + p.servingTimeStats.maxServingTime+
                        "\nAvg serving time " + p.servingTimeStats.totalServingTime / p.exitedElements +
                        "\nAvg parts in work " + p.avgWorkingCranes / timeCurrent + "\n\n\n");
                }
            });
        }
    }
}
