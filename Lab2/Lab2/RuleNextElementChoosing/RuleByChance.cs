using Lab3.Elements;
using Lab3.Helpers;
using Lab3.NextElementChoosingRules;

namespace Lab3.RuleNextElementChoosing
{
    public class RuleByChance : IRuleNextElementChoosing
    {
        public ProcessElement GetNextElement(List<(ProcessElement element, double chance)> nextElements)
        {
            return WeightedRandomHelper.GetRandomNextProcess(nextElements);
        }
    }
}
