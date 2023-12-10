using Lab3.GeneratingElements.Elements;
using Lab3.GeneratingElements.Generators;
using Lab3.Helpers.DistributionHelpers;
using Lab3.Helpers.Loggers;
using Lab3.NextElementChoosingRules;

namespace Lab3.Elements
{

    public class CreateElement : Element
    {

        private IElementsGenerator _elementsGenerator;

        public CreateElement(IDelayProvider delayProvider, IElementsGenerator generator, IRuleNextElementChoosing ruleNextElementChoosing, ILogger logger)
            : base(delayProvider, ruleNextElementChoosing, logger)
        {
            timeNext = 0.0;
            _elementsGenerator = generator;
        }

        public override void Exit()
        {
            var shipParts = _elementsGenerator.GenerateElement();

            timeNext = timeCurrent + delayProvider.GetDelay();

            exitedElements++;

            if (nextElements.Count != 0)
            {
                ProcessElement nextElement = ruleNextElementChoosing.GetNextElement(nextElements);
                nextElement.Enter(shipParts);
                _logger.Log("From " + elementName + " to " + nextElement.elementName + " exited ship");
            };
        }

        public override void PrintCurrentStat()
        {
            _logger.Log("Total exited elements from CREATOR: " + exitedElements + " time of next create " + timeNext);
        }

        public override void Enter((IGeneratedElement, IGeneratedElement) generatedElement) { }
        public override void EvaluateStats(double delta){ }

    }
}
