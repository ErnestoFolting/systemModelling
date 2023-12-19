using GraphPlottingApp;
using Lab3.GeneratingElements.Elements;
using Lab3.Helpers.DistributionHelpers;
using Lab3.Helpers.Loggers;
using Lab3.Helpers.Statistics;
using Lab3.NextElementChoosingRules;

namespace Lab3.Elements
{
    public class SimpleProcessElement : Element
    {
        public List<(IGeneratedElement part1, IGeneratedElement part2)> queue = new();
        public int maxQueueSize { get; set; }
        public int failureElements { get; private set; }
        public double meanQueueSize { get; private set; }
        public double timeInWork { get; private set; }
        public double avgWorkingParts { get; private set; }
        public static int queueChanges { get; set; }
        public ServingTimeStats servingTimeStats { get; private set; } = new();
        public override double timeNext
        {
            get
            {
                return cranes.Count > 0 ? cranes.Min(el => el.timeNext) : double.MaxValue;
            }
        }
        public bool isServing
        {
            get
            {
                return cranes.Count > 0 ? cranes.Any(el => el.isServing) : false;
            }
            protected set { }
        }

        public bool isFullLoaded
        {
            get
            {
                return cranes.Count > 0 ? cranes.All(el => el.isServing) : false;
            }
            protected set { }
        }

        public List<Crane> cranes { get; private set; } = new();
        public SimpleProcessElement(IDelayProvider delayProvider, int processPartsCount, IRuleNextElementChoosing ruleNextElementChoosing, ILogger logger) : base(delayProvider, ruleNextElementChoosing, logger)
        {
            meanQueueSize = 0.0;
            avgWorkingParts = 0.0;
            base.timeNext = double.MaxValue;
            for (int i = 0; i < processPartsCount; i++)
            {
                cranes.Add(new Crane(i));
            }
        }

        public override void Enter((IGeneratedElement part1, IGeneratedElement part2) shipParts)
        {
            if (!isFullLoaded)
            {
                Crane? part = cranes.Find(el => !el.isServing); //find first free part
                double partNextTime = timeCurrent + getDelay();
                part.timeNext = partNextTime;
                part.isServing = true;
                part.shipPartOnServing1 = shipParts.part1;
                part.shipPartOnServing2 = shipParts.part2;
            }
            else
            {
                if (queue.Count < maxQueueSize)
                {
                    queue.Add(shipParts);
                }
                else
                {
                    failureElements++;
                }
            }
        }

        public override void Exit()
        {
            var partsToExit = cranes.FindAll(el => el.timeNext == timeNext);
            exitedElements += partsToExit.Count();

            (IGeneratedElement part1, IGeneratedElement part2) exitedElement = (partsToExit.FirstOrDefault()?.shipPartOnServing1, partsToExit.FirstOrDefault()?.shipPartOnServing2);

            partsToExit.ForEach(el =>
            {
                el.timeNext = double.MaxValue;
                el.isServing = false;
                el.shipPartOnServing1 = null;
                el.shipPartOnServing2 = null;
            });

            //serving time stats evaluation
            double servingTime = exitedElement.part1.GetTimeOfServing(timeCurrent);
            if (servingTimeStats.minServingTime > servingTime) servingTimeStats.minServingTime = servingTime;
            if (servingTimeStats.maxServingTime < servingTime)
            {
                servingTimeStats.maxServingTime = servingTime;
            }

            servingTimeStats.totalServingTime += servingTime;

            //take new element from the queue
            if (queue.Count > 0)
            {
                (IGeneratedElement part1, IGeneratedElement part2) elementToServe;

                elementToServe = queue.FirstOrDefault();
                queue.Remove(elementToServe);

                Crane part = cranes.Find(el => !el.isServing); //find first free part
                double partNextTime = timeCurrent + getDelay();
                part.timeNext = partNextTime;
                part.isServing = true;
                part.shipPartOnServing1 = elementToServe.part1;
                part.shipPartOnServing2 = elementToServe.part2;
            }
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
                avgWorkingParts += cranes.Count(el => el.isServing) * delta;
            }
        }

        public override void PrintCurrentStat()
        {
            string statOfServing = isServing ? " is serving " : " is waiting ";
            _logger.Log(elementName + statOfServing + " already served " + exitedElements + " time of next exit " + timeNext);

            cranes.ForEach(el =>
            {
                _logger.Log($"\nCargo crane {el.id}:");
                if (el.shipPartOnServing1 != null)
                {
                    _logger.Log("part 1 : " + el.shipPartOnServing1.GetElementID());
                }
                else
                {
                    _logger.Log("part 1 : empty");
                }
                if (el.shipPartOnServing2 != null)
                {
                    _logger.Log("part 2 : " + el.shipPartOnServing2.GetElementID());
                }
                else
                {
                    _logger.Log("part 2 : empty");
                }
                _logger.Log("timeNext: " + el.timeNext + "\n");
            });
        }
    }
}
