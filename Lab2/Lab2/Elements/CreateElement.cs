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
        
        public static Dictionary<GeneratedElementTypeEnum, int> CountDict = new Dictionary<GeneratedElementTypeEnum, int>()
        {
            {GeneratedElementTypeEnum.Type1,0},
            {GeneratedElementTypeEnum.Type2,0},
            {GeneratedElementTypeEnum.Type3,0}
        };

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

            CountDict[generatedElement.GetType()]++;

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
                        Console.WriteLine("From " + elementName + " to " + nextElement.elementName + " exited " + generatedElement.GetType());
                        break;

                    default:
                        ProcessElement next = WeightedRandomHelper.GetRandomNextProcess(nextElements);
                        next.Enter(generatedElement);
                        Console.WriteLine("From " + elementName + " to " + next.elementName + " exited " + generatedElement.GetType());
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
