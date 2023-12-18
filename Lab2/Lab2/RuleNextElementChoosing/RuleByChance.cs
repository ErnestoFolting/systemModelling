using Lab3.Elements;
using Lab3.Helpers;
using Lab3.NextElementChoosingRules;

namespace Lab3.RuleNextElementChoosing
{
    public class RuleByChance : IRuleNextElementChoosing
    {
        public Element GetNextElement(List<(Element element, double chance)> nextElements)
        {
            return WeightedRandomHelper.GetRandomNextProcess(nextElements);
        }
    }
}
