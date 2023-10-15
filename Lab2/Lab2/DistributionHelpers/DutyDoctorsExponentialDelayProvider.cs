using Lab3.Enums;

namespace Lab3.DistributionHelpers
{
    public class DutyDoctorsExponentialDelayProvider : IDelayProvider
    {
        public DutyDoctorsExponentialDelayProvider()
        {
            
        }
        public double GetDelay()
        {
            throw new NotImplementedException();
        }

        public double GetDelayByType(GeneratedElementTypeEnum elementType)
        {
            return elementType switch
            {
                GeneratedElementTypeEnum.Type1 => GetDelayWithParams(15),
                GeneratedElementTypeEnum.Type2 => GetDelayWithParams(40),
                GeneratedElementTypeEnum.Type3 => GetDelayWithParams(30),
                _ => 0
            };
        }

        private double GetDelayWithParams(double timeMean)
        {
            double a = 0;
            Random rand = new Random();
            while (a == 0)
            {
                a = rand.NextDouble();
            }
            a = -timeMean * Math.Log(a);
            return a;
        }
    }
}
