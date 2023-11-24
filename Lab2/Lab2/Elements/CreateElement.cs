using Lab3.DistributionHelpers;
using Lab3.GeneratingElements.Elements;
using Lab3.GeneratingElements.Generators;
using Lab3.NextElementChoosingRules;

namespace Lab3.Elements
{

    public class CreateElement : Element
    {

        private IElementsGenerator _elementsGenerator;
        
        public CreateElement(IDelayProvider delayProvider, IElementsGenerator generator, IRuleNextElementChoosing ruleNextElementChoosing)
            : base(delayProvider, ruleNextElementChoosing)
        {
            timeNext = 0.0;
            _elementsGenerator = generator;
        }

        public override void Enter((IGeneratedElement, IGeneratedElement) generatedElement)
        {

        }

        public override void Exit()
        {
            timeNext = timeCurrent + delayProvider.GetDelay();

            var shipParts = _elementsGenerator.GenerateElement();

            exitedElements++;

            if (nextElements.Count != 0)
            {
                ProcessElement nextElement = ruleNextElementChoosing.GetNextElement(nextElements);
                nextElement.Enter(shipParts);
                Console.WriteLine("From " + elementName + " to " + nextElement.elementName + " exited " + shipParts.part1.GetType());
            };
        }

        public override void EvaluateStats(double delta)
        {

        }

        public override void PrintCurrentStat()
        {
            Console.WriteLine("Total exited elements from CREATOR: " + exitedElements + " time of next create " + timeNext);
        }

    }
}
