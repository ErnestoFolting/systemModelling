namespace Lab3.DistributionHelpers
{
    public class EqualDelayProvider : IDelayProvider
    {
        private double leftLimit;
        private double rightLimit;
        public EqualDelayProvider(double leftLimit, double rightLimit)
        {
            this.leftLimit = leftLimit;
            this.rightLimit = rightLimit;
        }
        public double GetDelay()
        {
            double multiplier = rightLimit - leftLimit;
            Random rand = new Random();
            return rand.NextDouble() * multiplier + leftLimit;
        }
    }
}
