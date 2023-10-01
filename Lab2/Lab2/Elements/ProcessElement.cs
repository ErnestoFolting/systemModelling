using Lab2.DistributionHelpers;
using System.Text;

namespace Lab2.Elements
{
    public class ProcessElement : Element
    {
        public int currentQueueSize{  get; private set; }
        public int maxQueueSize {  get; set; }
        public int failureElements{  get; private set; }
        public double meanQueueSize{  get; private set; }
        public double timeInWork { get; private set; }
        public ProcessElement(IDelayProvider delayProvider) : base(delayProvider)
        {
            currentQueueSize = 0;
            meanQueueSize = 0.0;
            base.timeNext = double.MaxValue;
        }

        public override void Enter()
        {
            if (!isServing)
            {
                isServing = true;
                timeNext = timeCurrent + getDelay();
            }
            else
            {
                if(currentQueueSize < maxQueueSize)
                {
                    currentQueueSize++;
                }
                else
                {
                    failureElements++;
                }
            }
        }

        public override void Exit()
        {
            base.Exit();
            timeNext = Double.MaxValue;
            isServing = false;
            if(currentQueueSize > 0)
            {
                currentQueueSize--;
                isServing = true;
                timeNext = timeCurrent + getDelay();
            }
            if (nextElement != null)nextElement.Enter();
        }

        public override void PrintStat()
        {
            base.PrintStat();
            Console.WriteLine("Failure elements " + failureElements);
        }

        public override void EvaluateStats(double delta)
        {
            meanQueueSize += currentQueueSize * delta;
            if (base.isServing) timeInWork += delta;
        }

        public override void PrintCurrentStat()
        {
            string statOfServing = isServing ? " is serving " : " is waiting ";
            Console.WriteLine(elementName + statOfServing + " already served " + exitedElements + " time of next exit " + timeNext);
        }
    }
}
