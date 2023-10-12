using Lab3.DistributionHelpers;
using Lab3.Enums;
using Lab3.Helpers;
using System.Text;

namespace Lab3.Elements
{
    public class ProcessElement : Element
    {
        public int currentQueueSize { get; private set; }
        public int maxQueueSize { get; set; }
        public int failureElements { get; private set; }
        public double meanQueueSize { get; private set; }
        public double timeInWork { get; private set; }
        public double avgWorkingParts { get; private set; }

        public override double timeNext
        {
            get
            {
                return processParts.Count > 0 ? processParts.Min(el => el.timeNext) : double.MaxValue;
            }
        }
        public bool isServing
        {
            get
            {
                return processParts.Count > 0 ? processParts.Any(el => el.isServing) : false;
            }
            protected set { }
        }

        public bool isFullLoaded
        {
            get
            {
                return processParts.Count > 0 ? processParts.All(el => el.isServing) : false;
            }
            protected set { }
        }

        public List<ProcessPart> processParts { get; private set; } = new();
        public ProcessElement(IDelayProvider delayProvider, int processPartsCount) : base(delayProvider)
        {
            currentQueueSize = 0;
            meanQueueSize = 0.0;
            avgWorkingParts = 0.0;
            base.timeNext = double.MaxValue;
            for (int i = 0; i < processPartsCount; i++)
            {
                processParts.Add(new ProcessPart(i));
            }
        }

        public override void Enter()
        {
            if (!isFullLoaded)
            {
                ProcessPart? part = processParts.Find(el => !el.isServing); //find first free part
                double partNextTime = timeCurrent + getDelay();
                part.timeNext = partNextTime;
                part.isServing = true;
            }
            else
            {
                if (currentQueueSize < maxQueueSize)
                {
                    currentQueueSize++;
                }
                else
                {
                    failureElements++;
                }
            }
        }

        public override void Exit(NextElementChoosingRule rule)
        {
            base.Exit(rule);

            Console.WriteLine(rule);

            var partsToExit = processParts.FindAll(el => el.timeNext == timeNext);
            exitedElements += partsToExit.Count() - 1; //because 1 added in base class

            partsToExit.ForEach(el =>
            {
                el.timeNext = double.MaxValue;
                el.isServing = false;
            });

            //take new element from the queue
            if (currentQueueSize > 0)
            {
                currentQueueSize--;
                ProcessPart part = processParts.Find(el => !el.isServing); //find first free part
                double partNextTime = timeCurrent + getDelay();
                part.timeNext = partNextTime;
                part.isServing = true;
            }

            //transfer element to the next ProcessElement
            if (nextElements.Count != 0)
            {
                ProcessElement next = WeightedRandomHelper.GetRandomNext(nextElements);
                next.Enter();
                Console.WriteLine("From " + elementName + " to " + next.elementName);
            };
        }

        public override void PrintStat()
        {
            base.PrintStat();
            Console.WriteLine("Failure elements " + failureElements);
        }

        public override void EvaluateStats(double delta)
        {
            meanQueueSize += currentQueueSize * delta;
            if (isServing)
            {
                timeInWork += delta;
                avgWorkingParts += processParts.Count(el => el.isServing) * delta;
            }
        }

        public override void PrintCurrentStat()
        {
            string statOfServing = isServing ? " is serving " : " is waiting ";
            Console.WriteLine(elementName + statOfServing + " already served " + exitedElements + " time of next exit " + timeNext);
        }
    }
}
