using Lab3.DistributionHelpers;
using Lab3.Elements;
using Lab3.Enums;
using Lab3.GeneratingElements.Elements;
using Lab3.GeneratingElements.Generators;
using Lab3.NextElementChoosingRules;
using Lab3.RuleNextElementChoosing;

namespace Lab3.Models
{
    public class BankModel : IModel
    {
        public List<Element> elements;
        private double avgClientsCount = 0;

        public BankModel()
        {
            IDelayProvider DelayGenerationClients = new ExponentialDelayProvider(0.3);
            IDelayProvider DelayServingClients = new ExponentialDelayProvider(0.3);

            IElementsGenerator elementGenerator = new DefaultElementsGenerator();

            IRuleNextElementChoosing ruleNextElementChoosing = new RuleByPriorityOrQueueSize();

            CreateElement createElement = new CreateElement(DelayGenerationClients, elementGenerator, ruleNextElementChoosing);
            ProcessElement processElement1 = new ProcessElement(DelayServingClients, 1, ruleNextElementChoosing);
            ProcessElement processElement2 = new ProcessElement(DelayServingClients, 1, ruleNextElementChoosing);

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
            Model model = new Model(elements);

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
            processElements.ForEach(el => clientsInMoment += el.queue.Count);
            avgClientsCount += clientsInMoment * delta;
        }

        private void ChangeQueue()
        {
            IEnumerable<ProcessElement> processElements = elements.OfType<ProcessElement>();
            ProcessElement? maxQueueSizeElement = processElements.OrderByDescending(el => el.queue.Count).FirstOrDefault();
            ProcessElement? minQueueSizeElement = processElements.OrderBy(el => el.queue.Count).FirstOrDefault();

            if (maxQueueSizeElement?.queue.Count - minQueueSizeElement?.queue.Count >= 2)
            {
                maxQueueSizeElement.queue.RemoveAt(1);
                minQueueSizeElement.queue.Add(new DefaultGeneratedElement(GeneratedElementTypeEnum.Type1));
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
