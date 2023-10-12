using Lab2.DistributionHelpers;
using Lab2.Helpers;

namespace Lab2.Elements
{
    public class CreateElement : Element
    {
        public CreateElement(IDelayProvider delayProvider) 
            : base(delayProvider)
        {
            timeNext = 0.0;
        }

        public override void Enter()
        {

        }

        public override void Exit()
        {
            base.Exit();
            timeNext = timeCurrent + getDelay();

            if (nextElements.Count != 0)
            {
                ProcessElement next = WeightedRandomHelper.GetRandomNext(nextElements);
                next.Enter();
                Console.WriteLine("From " + this.elementName + " to " + next.elementName);
            }
        }

        public override void EvaluateStats(double delta)
        {
            
        }

        public override void PrintCurrentStat()
        {
            Console.WriteLine("Total exited elements from CREATOR: " + exitedElements + " time of next create " + timeNext);
        }
    }
}
