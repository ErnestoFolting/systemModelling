using MathNet.Numerics.Distributions;

namespace Lab3.DistributionHelpers
{
    public class NormalDelayProvider : IDelayProvider
    {
        private Normal _delay;
        public NormalDelayProvider(double timeMean, double stdDev)
        {
            _delay = new Normal(timeMean, stdDev);
        }
        public double GetDelay()
        {
            return _delay.Sample();
        }
    }
}
