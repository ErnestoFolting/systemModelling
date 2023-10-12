using Lab3.DistributionHelpers;
using Lab3.Enums;

namespace Lab3.Elements
{
    public abstract class Element
    {
        public string elementName { get; set; }
        public virtual double timeNext { get; set; }
        public IDelayProvider delayProvider { get; private set; }
        public int exitedElements { get; protected set; }
        public double timeCurrent { get; set; }
        public List<(ProcessElement element, double chance)> nextElements { get; private set; } = new();
        public static int nextElementId { get; private set; }
        public int elementId { get; private set; }

        public Element(IDelayProvider delayProvider)
        {
            timeNext = 0.0;
            this.delayProvider = delayProvider;
            timeCurrent = timeNext;
            elementId = nextElementId;
            nextElementId++;
            elementName = "Element " + elementId;
        }

        public void AddNextElement(ProcessElement element, double chance)
        {
            nextElements.Add((element, chance));
        }

        public double getDelay()
        {
            return delayProvider.GetDelay();
        }

        public abstract void Enter();
        public virtual void Exit(NextElementChoosingRule rule)
        {
            exitedElements++;
        }
        public abstract void EvaluateStats(double delta);

        public virtual void PrintStat()
        {
            Console.WriteLine(elementName + " exited " + exitedElements + "\n");
        }

        public abstract void PrintCurrentStat();
    }
}
