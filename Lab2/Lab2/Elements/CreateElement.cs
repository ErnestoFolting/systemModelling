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
            timeNext = timeCurrent + getDelay();

            base.Exit(rule);
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
