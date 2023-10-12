using Lab3.DistributionHelpers;
using Lab3.Enums;
using Lab3.Helpers;

namespace Lab3.Elements
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

        public override void Exit(NextElementChoosingRule rule)
        {
            base.Exit(rule);
            Console.WriteLine(rule);
            timeNext = timeCurrent + getDelay();

            if (nextElements.Count != 0)
            {
                ProcessElement next = WeightedRandomHelper.GetRandomNext(nextElements);
                next.Enter();
                Console.WriteLine("From " + elementName + " to " + next.elementName);
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
