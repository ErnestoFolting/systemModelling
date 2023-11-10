using Lab3.Elements;
using Lab3.GeneratingElements.Elements;

namespace Lab3.NextElementChoosingRules
{
    public interface IRuleNextElementChoosing
    {
        public ProcessElement? GetNextElement(List<(ProcessElement element, double chance)> nextElements, IGeneratedElement exitedElement); 
    }
}
