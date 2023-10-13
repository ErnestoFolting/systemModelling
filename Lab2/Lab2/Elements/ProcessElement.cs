using Lab3.DistributionHelpers;
using Lab3.Enums;
using Lab3.GeneratingElements.Elements;
using Lab3.Helpers;

namespace Lab3.Elements
{
    public class ProcessElement : Element
    {
        public List<IGeneratedElement> queue = new List<IGeneratedElement>();
        public int maxQueueSize { get; set; }
        public int failureElements { get; private set; }
        public double meanQueueSize { get; private set; }
        public double timeInWork { get; private set; }
        public double avgWorkingParts { get; private set; }
        public static int queueChanges { get; set; }

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
            meanQueueSize = 0.0;
            avgWorkingParts = 0.0;
            base.timeNext = double.MaxValue;
            for (int i = 0; i < processPartsCount; i++)
            {
                processParts.Add(new ProcessPart(i));
            }
        }

        public override void Enter(IGeneratedElement generatedElement)
        {
            if (!isFullLoaded)
            {
                ProcessPart? part = processParts.Find(el => !el.isServing); //find first free part
                double partNextTime = timeCurrent + getDelay(generatedElement.GetType());
                part.timeNext = partNextTime;
                part.isServing = true;
                part.elementOnServing = generatedElement;
            }
            else
            {
                if (queue.Count < maxQueueSize)
                {
                    queue.Add(generatedElement);
                }
                else
                {
                    failureElements++;
                }
            }
        }

        public override void Exit(NextElementChoosingRule rule)
        {
            var partsToExit = processParts.FindAll(el => el.timeNext == timeNext);
            exitedElements += partsToExit.Count();

            partsToExit.ForEach(el =>
            {
                el.timeNext = double.MaxValue;
                el.isServing = false;
                el.elementOnServing = null;
            });

            IGeneratedElement? exitedElement = partsToExit.FirstOrDefault()?.elementOnServing;

            //take new element from the queue
            if (queue.Count > 0)
            {
                IGeneratedElement? elementToServe;
                
                if(queue.Any(el => el.GetPriority() == 1))
                {
                    elementToServe = queue.FirstOrDefault(el => el.GetPriority().Equals(1));
                }
                else
                {
                    elementToServe = queue.FirstOrDefault();

                }
                queue.Remove(elementToServe);

                ProcessPart part = processParts.Find(el => !el.isServing); //find first free part
                double partNextTime = timeCurrent + getDelay(elementToServe.GetType());
                part.timeNext = partNextTime;
                part.isServing = true;
                part.elementOnServing = elementToServe;
            }

            //transfer element to the next ProcessElement
            if (nextElements.Count != 0)
            {
                switch (rule)
                {
                    case NextElementChoosingRule.byPriorityOrQueueSize:
                        ProcessElement nextElement = nextElements.OrderBy(el => el.chance).FirstOrDefault().element; //first priority
                        if (nextElements.Any(el => el.element.queue.Count < nextElement.queue.Count))
                        {
                            nextElement = nextElements.OrderBy(el => el.element.queue.Count).FirstOrDefault().element; //min queue size
                        };
                        nextElement.Enter(exitedElement);
                        Console.WriteLine("From " + elementName + " to " + nextElement.elementName);
                        break;

                    default:
                        ProcessElement next = WeightedRandomHelper.GetRandomNextProcess(nextElements);
                        next.Enter(exitedElement);
                        Console.WriteLine("From " + elementName + " to " + next.elementName);
                        break;
                }
            };
        }

        public override void PrintStat()
        {
            base.PrintStat();
            Console.WriteLine("Failure elements " + failureElements);
        }

        public override void EvaluateStats(double delta)
        {
            meanQueueSize += queue.Count * delta;
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
