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

        public override void Enter(IGeneratedElement generatedElement)
        {

        }

        public override void Exit()
        {
            timeNext = timeCurrent + getDelayInCreate();

            IGeneratedElement generatedElement = _elementsGenerator.GenerateElement();

            generatedElement.SetGenerationTime(timeCurrent);

            exitedElements++;

            if (nextElements.Count != 0)
            {
                ProcessElement nextElement = ruleNextElementChoosing.GetNextElement(nextElements);
                nextElement.Enter(generatedElement);
                Console.WriteLine("From " + elementName + " to " + nextElement.elementName + " exited " + generatedElement.GetType());
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
