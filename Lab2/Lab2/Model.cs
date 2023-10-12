﻿using Lab3.Elements;
using Lab3.Enums;

namespace Lab3
{
    public class Model
    {
        private List<Element> elements = new();
        public double timeNext;
        public double timeCurrent;
        int nextEventId;
        private NextElementChoosingRule nextElementChoosingRule;


        public Model(List<Element> elements, NextElementChoosingRule nextElementChoosingRule)
        {
            this.elements = elements;
            timeNext = 0;
            nextEventId = 0;
            timeCurrent = timeNext;
            this.nextElementChoosingRule = nextElementChoosingRule;
        }

        public void Simulation(double timeOfSimulation)
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

                Element? nextElement = elements.Find(el => el.elementId == nextEventId);

                Console.WriteLine("\n\n\nNext will be event in " + nextElement?.elementName + " , time of this event = " + timeNext);
                foreach (Element element in elements)
                {
                    element.EvaluateStats(timeNext - timeCurrent);
                }

                timeCurrent = timeNext;

                elements.ForEach(el => el.timeCurrent = timeCurrent);

                elements.ForEach(el =>
                {
                    if (el.timeNext == timeCurrent) el.Exit(nextElementChoosingRule);
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
                    Console.WriteLine("\n\n" + p.elementName + ":");
                    Console.WriteLine("Mean queue length: " + p.meanQueueSize / timeCurrent +
                        "\nFailure probability: " + p.failureElements / (double)(p.exitedElements + p.failureElements) +
                        "\nLoading " + p.timeInWork / timeCurrent +
                        "\nAvg serving time " + p.timeInWork / p.exitedElements +
                        "\nAvg parts in work " + p.avgWorkingParts / timeCurrent + "\n\n\n");
                }
            });
        }
    }
}
