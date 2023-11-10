using MathNet.Numerics.Distributions;

namespace Lab3.DistributionHelpers
{
    public class ErlangDelayProvider : IDelayProvider
    {
        private double _timeMean;
        private int k;
        public ErlangDelayProvider(double timeMean, int k)
        {
            _timeMean = timeMean;
            this.k = k;
        }
        public double GetDelay()
        {
            Erlang erlang = new Erlang(k, Convert.ToDouble(k)/_timeMean);
            return erlang.Sample();
        }
    }
}
