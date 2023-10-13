using Lab3.Elements;
using Lab3.Enums;

namespace Lab3.Helpers
{
    public class WeightedRandomHelper
    {
        public static ProcessElement GetRandomNextProcess(List<(ProcessElement element, double chance)> nextElements)
        {
            Random rand = new Random();
            double chancesSum = nextElements.Sum(el => el.chance);
            double randValue = rand.NextDouble() * chancesSum;

            double chanceAcc = 0;
            for (int i = 0; i < nextElements.Count; i++)
            {
                chanceAcc += nextElements[i].chance;
                if (chanceAcc > randValue)
                {
                    return nextElements[i].element;
                }
            }
            return default;
        }

        public static GeneratedElementTypeEnum GetRandomGeneratedElementType(List<(GeneratedElementTypeEnum type, double chance)> generatedElementsTypes)
        {
            Random rand = new Random();
            double chancesSum = generatedElementsTypes.Sum(el => el.chance);
            double randValue = rand.NextDouble() * chancesSum;

            double chanceAcc = 0;
            for (int i = 0; i < generatedElementsTypes.Count; i++)
            {
                chanceAcc += generatedElementsTypes[i].chance;
                if (chanceAcc > randValue)
                {
                    return generatedElementsTypes[i].type;
                }
            }
            return default;
        }
    }
}
