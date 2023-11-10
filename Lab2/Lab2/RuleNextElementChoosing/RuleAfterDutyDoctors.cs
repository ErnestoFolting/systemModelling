using Lab3.Elements;
using Lab3.Enums;
using Lab3.GeneratingElements.Elements;
using Lab3.NextElementChoosingRules;

namespace Lab3.RuleNextElementChoosing
{
    public class RuleAfterDutyDoctors : IRuleNextElementChoosing
    {
        public ProcessElement GetNextElement(List<(ProcessElement element, double chance)> nextElements, IGeneratedElement exitedElement)
        {
            return exitedElement.GetType() switch
            {
                GeneratedElementTypeEnum.Type1 => nextElements.FirstOrDefault(el => el.element.elementName.Contains("WARD")).element,
                _ => nextElements.FirstOrDefault(el => !el.element.elementName.Contains("WARD")).element
            };
        }
    }
}