using Lab3.Elements;
using Lab3.Enums;
using Lab3.GeneratingElements.Elements;
using Lab3.NextElementChoosingRules;

namespace Lab3.RuleNextElementChoosing
{
    public class RuleAfterLabAnalysis : IRuleNextElementChoosing
    {
        public ProcessElement? GetNextElement(List<(ProcessElement element, double chance)> nextElements, IGeneratedElement exitedElement)
        {
            if(exitedElement.GetType() == GeneratedElementTypeEnum.Type2)
            {
                exitedElement.SetType(GeneratedElementTypeEnum.Type1);
                return nextElements.FirstOrDefault(el => el.element.elementName.Contains("DUTY")).element;
            }
            else
            {
                return null;
            }
        }
    }
}
