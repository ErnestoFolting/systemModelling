using Lab3.DistributionHelpers;
using Lab3.Enums;
using Lab3.GeneratingElements.Elements;
using Lab3.GeneratingElements.Generators;
using Lab3.Helpers;

namespace Lab3.Elements
{
    public class CreateElement : Element
    {

        private IElementsGenerator _elementsGenerator;
        public CreateElement(IDelayProvider delayProvider, IElementsGenerator generator)
            : base(delayProvider)
        {
            timeNext = 0.0;
            _elementsGenerator = generator;
        }

        public override void Enter(IGeneratedElement generatedElement)
        {

        }

        public override void Exit(NextElementChoosingRule rule)
        {
            timeNext = timeCurrent + getDelayInCreate();

            IGeneratedElement generatedElement = _elementsGenerator.GenerateElement();

            exitedElements++;
            if (nextElements.Count != 0)
            {
                switch (rule)
                {
                    case NextElementChoosingRule.byPriorityOrQueueSize:
                        ProcessElement nextElement = nextElements.OrderBy(el => el.chance).FirstOrDefault().element; //first priority
                        if (nextElements.Any(el => el.element.queue.Count < nextElement.queue.Count))
                        {
                            nextElement = nextElements.OrderBy(el => el.element.queue.Count).FirstOrDefault().element; //min queue size
                        };
                        nextElement.Enter(generatedElement);
                        Console.WriteLine("From " + elementName + " to " + nextElement.elementName);
                        break;

                    default:
                        ProcessElement next = WeightedRandomHelper.GetRandomNextProcess(nextElements);
                        next.Enter(generatedElement);
                        Console.WriteLine("From " + elementName + " to " + next.elementName);
                        break;
                }
            };
        }

        public override void EvaluateStats(double delta)
        {

        }

        public override void PrintCurrentStat()
        {
            Console.WriteLine("Total exited elements from CREATOR: " + exitedElements + " time of next create " + timeNext);
        }

        private double getDelayInCreate()
        {
            return delayProvider.GetDelay();
        }
    }
}
