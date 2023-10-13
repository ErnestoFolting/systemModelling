using Lab3.Enums;

namespace Lab3.GeneratingElements.Elements
{
    public class PatientGeneratedElement : IGeneratedElement
    {
        private int _priority;
        private GeneratedElementTypeEnum _type;
        public PatientGeneratedElement(GeneratedElementTypeEnum type)
        {
            _type = type;
            _priority = (_type == GeneratedElementTypeEnum.Type1 ? 1 : 2); 
        }
        public int GetPriority()
        {
            return _priority;
        }

        GeneratedElementTypeEnum IGeneratedElement.GetType()
        {
            return _type;
        }
    }
}
