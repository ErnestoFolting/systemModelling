namespace Lab3.Helpers.DistributionHelpers
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
            var res = rand.NextDouble() * multiplier + leftLimit;
            return res;
        }
    }
}
