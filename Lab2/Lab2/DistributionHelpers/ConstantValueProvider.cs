namespace Lab3.DistributionHelpers
{
    public class ConstantValueProvider : IDelayProvider
    {
        private double _delay;
        public ConstantValueProvider(double delay)
        {
            _delay = delay;
        }
        public double GetDelay()
        {
            return _delay;
        }
    }
}
