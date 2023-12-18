using Lab3.Elements;

namespace Lab3.NextElementChoosingRules
{
    public interface IRuleNextElementChoosing
    {
        public Element? GetNextElement(List<(Element element, double chance)> nextElements);
    }
}
