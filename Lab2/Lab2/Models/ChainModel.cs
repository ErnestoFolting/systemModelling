using Lab3.DistributionHelpers;
using Lab3.Elements;
using Lab3.GeneratingElements.Elements;
using Lab3.GeneratingElements.Generators;
using Lab3.NextElementChoosingRules;
using Lab3.RuleNextElementChoosing;

namespace Lab3.Models
{
    public class ChainModel : IModel
    {
        private int N;

        public List<Element> elements = new();

        public ChainModel(int N)
        {
            this.N = N;

            ExponentialDelayProvider delayProvider = new ExponentialDelayProvider(0.5);

            IElementsGenerator elementGenerator = new DefaultElementsGenerator();

            IRuleNextElementChoosing ruleNextElementChoosing = new RuleByPriorityOrQueueSize();

            CreateElement createElement = new CreateElement(delayProvider, elementGenerator, ruleNextElementChoosing);
            Element prevEL = createElement;
            elements.Add(createElement);
            for (int i = 0; i < N; i++)
            {
                ProcessElement processElement = new ProcessElement(delayProvider, 1, ruleNextElementChoosing);
                elements.Add(processElement);
                prevEL.nextElements.Add((processElement, 1));

                IGeneratedElement el = new DefaultGeneratedElement(Enums.GeneratedElementTypeEnum.Type1);
                processElement.processParts[0].timeNext = delayProvider.GetDelay();
                processElement.processParts[0].elementOnServing = el;
                processElement.processParts[0].isServing = true;

                processElement.maxQueueSize = 1000;
                processElement.elementName = "PROCESS" + (i + 1);
                prevEL = processElement;
            }
        }
        public void StartSimulation()
        {
            Model model = new Model(elements);

            model.Simulation(500, null);
        }
    }
}
