namespace Lab2.DistributionHelpers
{
    public class ExponentialDelayProvider : IDelayProvider
    {
        private double _timeMean;
        public ExponentialDelayProvider(double timeMean)
        {
            _timeMean = timeMean;
        }

        public double GetDelay()
        {
            double a = 0;
            Random rand = new Random();
            while (a == 0)
            {
                a = rand.NextDouble();
            }
            a = -_timeMean * Math.Log(a);
            return a;
        }
    }
}
