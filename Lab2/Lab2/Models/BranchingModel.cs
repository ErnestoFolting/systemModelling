using Lab3.DistributionHelpers;
using Lab3.Elements;
using Lab3.GeneratingElements.Elements;
using Lab3.GeneratingElements.Generators;
using Lab3.NextElementChoosingRules;
using Lab3.RuleNextElementChoosing;

namespace Lab3.Models
{
    public class BranchingModel : IModel
    {
        private int N;

        public List<Element> elements = new();

        public BranchingModel(int N)
        {
            this.N = N;

            ExponentialDelayProvider delayProvider = new ExponentialDelayProvider(0.5);

            IElementsGenerator elementGenerator = new DefaultElementsGenerator();

            IRuleNextElementChoosing ruleNextElementChoosing = new RuleByPriorityOrQueueSize();

            CreateElement createElement = new CreateElement(delayProvider, elementGenerator, ruleNextElementChoosing);
            Element prevEL1 = createElement;
            Element prevEL2 = createElement;
            elements.Add(createElement);
            for (int i = 0; i < N / 2; i++)
            {
                ProcessElement processElement1 = new ProcessElement(delayProvider, 1, ruleNextElementChoosing);
                ProcessElement processElement2 = new ProcessElement(delayProvider, 1, ruleNextElementChoosing);

                elements.Add(processElement1);
                elements.Add(processElement2);

                if (i == 0)
                {
                    prevEL1.nextElements.Add((processElement1, 0.5));
                    prevEL1.nextElements.Add((processElement2, 0.5));
                }
                else
                {
                    prevEL1.nextElements.Add((processElement1, 1));
                    //prevEL1.nextElements.Add((processElement1, 0.5));
                    prevEL2.nextElements.Add((processElement2, 1));
                    //prevEL2.nextElements.Add((processElement2, 0.5));
                }
                IGeneratedElement el = new DefaultGeneratedElement(Enums.GeneratedElementTypeEnum.Type1);
                processElement1.processParts[0].timeNext = delayProvider.GetDelay();
                processElement1.processParts[0].elementOnServing = el;
                processElement1.processParts[0].isServing = true;

                processElement1.maxQueueSize = 1000;
                processElement1.elementName = "PROCESS 1 " + (i + 1);

                processElement2.processParts[0].timeNext = delayProvider.GetDelay();
                processElement2.processParts[0].elementOnServing = el;
                processElement2.processParts[0].isServing = true;

                processElement2.maxQueueSize = 1000;
                processElement2.elementName = "PROCESS 2 " + (i + 1);

                prevEL1 = processElement1;
                prevEL2 = processElement2;
            }
        }
        public void StartSimulation()
        {
            Model model = new Model(elements);

            model.Simulation(500, null);
        }
    }
}
