using Lab2.DistributionHelpers;

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
            nextElement.Enter();
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
