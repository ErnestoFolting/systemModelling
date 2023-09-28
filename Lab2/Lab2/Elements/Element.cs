using Lab2.DistributionHelpers;
using Lab2.Enums;

namespace Lab2.Elements
{
    public abstract class Element
    {
        public string elementName { get;  set; }
        public double timeNext { get; protected set; }
        public IDelayProvider delayProvider { get; private set; }
        public int exitedElements { get; protected set; }
        public double timeCurrent { get; set; }
        public bool isServing { get; protected set; }
        public Element nextElement { get; set; }
        public static int nextElementId { get; private set; }
        public int elementId{ get; private set; }

        public Element(IDelayProvider delayProvider)
        {
            timeNext = 0.0;
            this.delayProvider = delayProvider;
            timeCurrent = timeNext;
            isServing = false;
            nextElement = null;
            elementId = nextElementId;
            nextElementId++;
            elementName = "Element " + elementId;
        }

        public double getDelay()
        {
            return delayProvider.GetDelay();
        }

        public abstract void Enter();
        public virtual void Exit()
        {
            exitedElements++;;
        }
        public abstract void EvaluateStats(double delta);

        public virtual void PrintStat()
        {
            Console.WriteLine(elementName + " exited " + exitedElements);
        }

        public abstract void PrintCurrentStat();
    }
}
