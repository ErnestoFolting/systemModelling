using Lab3.Enums;

namespace Lab3.GeneratingElements.Elements
{
    public class DefaultGeneratedElement : IGeneratedElement
    {
        private int _priority;
        private GeneratedElementTypeEnum _type;
        public DefaultGeneratedElement(GeneratedElementTypeEnum type)
        {
            _type = type;
            _priority = 1;
        }
        public int GetPriority()
        {
            return _priority;
        }

        public GeneratedElementTypeEnum GetType()
        {
            return _type;
        }

        public void SetType(GeneratedElementTypeEnum newType)
        {
            _type = newType;
        }
    }
}
