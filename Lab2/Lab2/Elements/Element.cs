using Lab3.DistributionHelpers;
using Lab3.Enums;
using Lab3.Helpers;

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
            if (nextElements.Count != 0)
            {
                switch (rule)
                {
                    case NextElementChoosingRule.byPriority:
                        ProcessElement nextElement = nextElements.OrderBy(el => el.chance).FirstOrDefault().element; //first priority
                        if (nextElements.Any(el => el.element.currentQueueSize < nextElement.currentQueueSize))
                        {
                            nextElement = nextElements.OrderBy(el => el.element.currentQueueSize).FirstOrDefault().element; //min queue size
                        };
                        nextElement.Enter();
                        Console.WriteLine("From " + elementName + " to " + nextElement.elementName);
                        break;

                    default:
                        ProcessElement next = WeightedRandomHelper.GetRandomNext(nextElements);
                        next.Enter();
                        Console.WriteLine("From " + elementName + " to " + next.elementName);
                        break;
                }
            };
        }
        public abstract void EvaluateStats(double delta);

        public virtual void PrintStat()
        {
            Console.WriteLine(elementName + " exited " + exitedElements + "\n");
        }

        public abstract void PrintCurrentStat();
    }
}
