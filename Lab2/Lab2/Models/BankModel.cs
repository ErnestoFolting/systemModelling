using Lab3.DistributionHelpers;
using Lab3.Elements;
using Lab3.Enums;

namespace Lab3.Models
{
    public class BankModel
    {
        public List<Element> elements;
        private double avgClientsCount = 0;

        public BankModel()
        {
            IDelayProvider ExponentialDelayProvider1 = new ExponentialDelayProvider(0.3);
            IDelayProvider ExponentialDelayProvider2 = new ExponentialDelayProvider(0.3);

            CreateElement createElement = new CreateElement(ExponentialDelayProvider1);
            ProcessElement processElement1 = new ProcessElement(ExponentialDelayProvider2, 1);
            ProcessElement processElement2 = new ProcessElement(ExponentialDelayProvider2, 1);

            createElement.AddNextElement(processElement1, 1);
            createElement.AddNextElement(processElement2, 1);

            processElement1.maxQueueSize = 3;  // max queue size
            processElement2.maxQueueSize = 3;  // max queue size

            createElement.elementName = "CREATOR";
            processElement1.elementName = "PROCESSOR1";
            processElement2.elementName = "PROCESSOR2";


            elements = new List<Element>() { createElement, processElement1, processElement2 };

        }
        public void StartSimulation()
        {
            Model model = new Model(elements, NextElementChoosingRule.byPriority);

            model.Simulation(1000, ActionPerIteration);
            OutputStats();
        }

        private void ActionPerIteration(double delta)
        {
            ChangeQueue();
            EvaluateStats(delta);
        }

        private void EvaluateStats(double delta)
        {
            double clientsInMoment = 0;
            List<ProcessElement> processElements = elements.OfType<ProcessElement>().ToList();
            clientsInMoment += processElements.Count(el => el.isServing);
            processElements.ForEach(el => clientsInMoment += el.currentQueueSize);
            avgClientsCount += clientsInMoment * delta;
        }

        private void ChangeQueue()
        {
            IEnumerable<ProcessElement> processElements = elements.OfType<ProcessElement>();
            ProcessElement? maxQueueSizeElement = processElements.OrderByDescending(el => el.currentQueueSize).FirstOrDefault();
            ProcessElement? minQueueSizeElement = processElements.OrderBy(el => el.currentQueueSize).FirstOrDefault();

            if (maxQueueSizeElement?.currentQueueSize - minQueueSizeElement?.currentQueueSize >= 2)
            {
                maxQueueSizeElement.currentQueueSize--;
                minQueueSizeElement.currentQueueSize++;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("****************************************Element changed the queue****************************************");
                Console.ForegroundColor = ConsoleColor.Gray;
                ProcessElement.queueChanges++;
            }
        }

        private void OutputStats()
        {
            List<ProcessElement> processElements = elements.OfType<ProcessElement>().ToList();
            List<CreateElement> createElements = elements.OfType<CreateElement>().ToList();

            double totalServed = processElements.Sum(el => el.exitedElements);

            double totalTimeInWork = processElements.Sum(el => el.timeInWork);
            double totalTimeInQueue = processElements.Sum(el => el.meanQueueSize);
            double totalElementsGenerated = createElements.Sum(el => el.exitedElements);

            double avgTimeSpendInSystem = (totalTimeInWork + totalTimeInQueue) / totalElementsGenerated;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\n\nBank Stats");
            Console.WriteLine("Queue changes: " + ProcessElement.queueChanges);
            Console.WriteLine("AvgClientsCount: " + avgClientsCount / elements[0].timeCurrent);
            Console.WriteLine("AvgIntervalsBetweenServed: " + elements[0].timeCurrent / totalServed);
            Console.WriteLine("AvgTimeSpendInSystem: " + avgTimeSpendInSystem);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
