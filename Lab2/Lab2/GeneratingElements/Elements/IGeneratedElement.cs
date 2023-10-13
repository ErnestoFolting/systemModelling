using Lab3.Enums;

namespace Lab3.GeneratingElements.Elements
{
    public interface IGeneratedElement
    {
        public int GetPriority();
        public GeneratedElementTypeEnum GetType();
    }
}
