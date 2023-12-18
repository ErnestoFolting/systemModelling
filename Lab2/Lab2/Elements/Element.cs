using Lab3.GeneratingElements.Elements;
using Lab3.Helpers.DistributionHelpers;
using Lab3.Helpers.Loggers;
using Lab3.NextElementChoosingRules;

namespace Lab3.Elements
{
    public abstract class Element
    {
        public static int nextElementId { get; private set; }

        protected ILogger _logger;
        public string elementName { get; set; }
        public virtual double timeNext { get; set; }
        public IDelayProvider delayProvider { get; private set; }
        public int exitedElements { get; protected set; }
        public double timeCurrent { get; set; }
        public List<(Element element, double chance)> nextElements { get; private set; } = new();
        public int elementId { get; private set; }
        public IRuleNextElementChoosing ruleNextElementChoosing { get; private set; }

        public Element(IDelayProvider delayProvider, IRuleNextElementChoosing ruleNextElementChoosing, ILogger logger)
        {
            timeNext = 0.0;
            this.delayProvider = delayProvider;
            timeCurrent = timeNext;
            elementId = nextElementId;
            nextElementId++;
            elementName = "Element " + elementId;
            this.ruleNextElementChoosing = ruleNextElementChoosing;
            this._logger = logger;
        }

        public void AddNextElement(Element element, double chance)
        {
            nextElements.Add((element, chance));
        }

        public double getDelay()
        {
            return delayProvider.GetDelay();
        }

        public abstract void Enter((IGeneratedElement part1, IGeneratedElement part2) generatedElement);

        public abstract void Exit();

        public abstract void EvaluateStats(double delta);

        public virtual void PrintStat()
        {
            _logger.Log(elementName + " exited " + exitedElements + "\n");
        }

        public abstract void PrintCurrentStat();
    }
}
