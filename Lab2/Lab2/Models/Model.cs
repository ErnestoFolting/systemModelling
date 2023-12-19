using GraphPlottingApp;
using Lab3.Elements;
using Lab3.Helpers.Loggers;
using Lab3.Helpers.Statistics;

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

        public SimulationStats Simulation(double timeOfSimulation, Action<double>? actionPerIteration)
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
            return PrintResult();
        }

        private void PrintCurrentStats()
        {
            elements.ForEach(el => el.PrintCurrentStat());
        }

        private SimulationStats PrintResult()
        {
            SimulationStats stats = new SimulationStats();
            logger.Log("\n********************************Results********************************");
            elements.ForEach(el =>
            {
                el.PrintStat();
                if (el.GetType() == typeof(ProcessElement))
                {
                    ProcessElement p = (ProcessElement)el;
                    logger.Log("\n\n" + p.elementName + ":");

                    PlotHelper.statsPlotsData.Add(p.statsPairs);

                    
                    stats.meanQueueLength = p.meanQueueSize / timeCurrent;
                    stats.failureProbability = p.failureElements / (double)(p.exitedElements + p.failureElements);
                    stats.loading = p.timeInWork / timeCurrent;
                    stats.minServingTime = p.servingTimeStats.minServingTime;
                    stats.maxServingTime = p.servingTimeStats.maxServingTime;
                    stats.avgServingTime = p.servingTimeStats.totalServingTime / p.exitedElements;
                    stats.avgWorkingParts = p.avgWorkingCranes / timeCurrent;

                    Console.WriteLine("Mean queue length: " + stats.meanQueueLength +
                        "\nLoading " + stats.loading +
                        "\nAvg working parts " + stats.avgWorkingParts +
                        "\nMin serving time " + stats.minServingTime +
                        "\nMax serving time " + stats.maxServingTime +
                        "\nAvg serving time " + stats.avgServingTime);
                }
                else
                {
                    if (el.GetType() == typeof(SimpleProcessElement))
                    {
                        SimpleProcessElement p = (SimpleProcessElement)el;
                        logger.Log("\n\n" + p.elementName + ":");


                        stats.meanQueueLength = p.meanQueueSize / timeCurrent;
                        stats.failureProbability = p.failureElements / (double)(p.exitedElements + p.failureElements);
                        stats.loading = p.timeInWork / timeCurrent;
                        stats.minServingTime = p.servingTimeStats.minServingTime;
                        stats.maxServingTime = p.servingTimeStats.maxServingTime;
                        stats.avgServingTime = p.servingTimeStats.totalServingTime / p.exitedElements;
                        stats.avgWorkingParts = p.avgWorkingParts / timeCurrent;

                        Console.WriteLine("Mean queue length: " + stats.meanQueueLength +
                            "\nLoading " + stats.loading +
                            "\nAvg working parts " + stats.avgWorkingParts +
                            "\nMin serving time " + stats.minServingTime +
                            "\nMax serving time " + stats.maxServingTime +
                            "\nAvg serving time " + stats.avgServingTime);
                    }
                }
            });
            return stats;
        }
    }
}
