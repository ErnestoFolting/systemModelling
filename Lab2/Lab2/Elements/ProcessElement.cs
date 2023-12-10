﻿using Lab3.GeneratingElements.Elements;
using Lab3.Helpers.DistributionHelpers;
using Lab3.Helpers.Loggers;
using Lab3.NextElementChoosingRules;

namespace Lab3.Elements
{
    public class ProcessElement : Element
    {
        public List<(IGeneratedElement part1, IGeneratedElement part2)> queue = new List<(IGeneratedElement part1, IGeneratedElement part2)>();
        public int maxQueueSize { get; set; }
        public int failureElements { get; private set; }
        public double meanQueueSize { get; private set; }
        public double timeInWork { get; private set; }
        public double avgWorkingCranes { get; private set; }

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

        public bool isFullFree
        {
            get
            {
                return cranes.Count > 0 ? !cranes.Any(el => el.isServing) : false;
            }
            protected set { }
        }

        public List<Crane> cranes { get; private set; } = new();
        public ProcessElement(IDelayProvider delayProvider, int processPartsCount, IRuleNextElementChoosing ruleNextElementChoosing, ILogger logger) : base(delayProvider, ruleNextElementChoosing, logger)
        {
            meanQueueSize = 0.0;
            avgWorkingCranes = 0.0;
            base.timeNext = double.MaxValue;
            for (int i = 0; i < processPartsCount; i++)
            {
                cranes.Add(new Crane(i));
            }
        }

        public override void Enter((IGeneratedElement part1, IGeneratedElement part2) shipParts)
        {
            var crane1 = cranes[0];
            var crane2 = cranes[1];

            double fullDelay = getDelay();
            _logger.Log("Full delay " + fullDelay);

            if (isFullFree) // no ships in haven
            {
                double partNextTime = timeCurrent + fullDelay / 2;

                cranes.ForEach(crane =>
                {
                    crane.timeNext = partNextTime;
                    crane.isServing = true;
                });

                crane1.shipPartOnServing1 = shipParts.part1;
                crane2.shipPartOnServing1 = shipParts.part2;
            }
            else
            {
                if (crane1.shipPartOnServing1.GetElementID() == crane2.shipPartOnServing1.GetElementID()) // 1 ship was serving on both
                {
                    crane1.shipPartOnServing2 = crane2.shipPartOnServing1; //make crane1 serve full ship
                    crane1.timeNext = timeCurrent + (crane1.timeNext - timeCurrent) * 2;

                    crane2.shipPartOnServing1 = shipParts.part1; //make crane2 serve full new ship
                    crane2.shipPartOnServing2 = shipParts.part2;
                    crane2.timeNext = timeCurrent + fullDelay;
                }
                else if(queue.Count < maxQueueSize) // 2 ships is serving
                {
                    queue.Add(shipParts);
                }
                else // 2 ships is serving and the queue is full
                {
                    failureElements++;
                }
            }
        }

        public override void Exit()
        {
            List<Crane> cranesToExit = cranes.FindAll(el => el.timeNext == timeNext);
            int? exitedShipId = cranesToExit.FirstOrDefault()?.shipPartOnServing1.GetElementID();

            exitedElements++;

            cranesToExit.ForEach(el =>
            {
                el.timeNext = double.MaxValue;
                el.isServing = false;
                el.shipPartOnServing1 = null;
                el.shipPartOnServing2 = null;
            });


            if(queue.Count == 0)
            {
                if (cranesToExit.Count == 1) // if was two ships in haven, should divide the ship parts
                {
                    Crane exitPart = cranesToExit[0];
                    Crane? craneInProcess = cranes.FirstOrDefault(el => el.id != exitPart.id);

                    exitPart.isServing = true;
                    exitPart.shipPartOnServing1 = craneInProcess.shipPartOnServing2;
                    craneInProcess.shipPartOnServing2 = null;

                    double newTimeNext = timeCurrent + (craneInProcess.timeNext - timeCurrent ) / 2;
                    craneInProcess.timeNext = newTimeNext;
                    exitPart.timeNext = newTimeNext;
                }
            }
            else
            {
                (IGeneratedElement part1, IGeneratedElement part2) shipToServe = queue.FirstOrDefault();

                queue.Remove(shipToServe);

                Crane? freeCrane = cranes.FirstOrDefault(el => !el.isServing);
                freeCrane.isServing = true;
                freeCrane.shipPartOnServing1 = shipToServe.part1;
                freeCrane.shipPartOnServing2 = shipToServe.part2;
                double fullDelay = getDelay();
                freeCrane.timeNext = timeCurrent + fullDelay;
                _logger.Log("Full delay " + fullDelay);
            }        
        }

        public override void PrintStat()
        {
            base.PrintStat();
            _logger.Log("Failure elements " + failureElements);
        }

        public override void EvaluateStats(double delta)
        {
            meanQueueSize += queue.Count * delta;
            if (isServing)
            {
                if (cranes[0].shipPartOnServing1.GetElementID() != cranes[1].shipPartOnServing1.GetElementID())
                {
                    timeInWork += delta;
                }
                timeInWork += delta;
                avgWorkingCranes += cranes.Count(el => el.isServing) * delta;
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
