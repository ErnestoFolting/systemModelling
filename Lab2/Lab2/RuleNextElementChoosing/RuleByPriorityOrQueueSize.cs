using Lab3.Elements;
using Lab3.GeneratingElements.Elements;
using Lab3.NextElementChoosingRules;

namespace Lab3.RuleNextElementChoosing
{
    public class RuleByPriorityOrQueueSize : IRuleNextElementChoosing
    {
        public ProcessElement GetNextElement(List<(ProcessElement element, double chance)> nextElements, IGeneratedElement exitedElement)
        {
            ProcessElement nextElement = nextElements.OrderBy(el => el.chance).FirstOrDefault().element; //first priority
            if (nextElements.Any(el => el.element.queue.Count < nextElement.queue.Count))
            {
                nextElement = nextElements.OrderBy(el => el.element.queue.Count).FirstOrDefault().element; //min queue size
            };
            return nextElement;
        }
    }
}
