using Lab3.Enums;

namespace Lab3.GeneratingElements.Elements
{
    public class PatientGeneratedElement : IGeneratedElement
    {
        private int _priority;
        private GeneratedElementTypeEnum _type;
        private double _generatedTime;
        private bool _typeChanged;
        public PatientGeneratedElement(GeneratedElementTypeEnum type)
        {
            _type = type;
            _priority = (_type == GeneratedElementTypeEnum.Type1 ? 1 : 2); 
        }

        public bool GetTypeChanged()
        {
            return _typeChanged;
        }

        public int GetPriority()
        {
            return _priority;
        }

        public double GetTimeDifference(double finishTime)
        {
            return finishTime - _generatedTime;
        }

        public void SetGenerationTime(double generationTime)
        {
            this._generatedTime = generationTime;
        }

        public void SetTypeChanged(bool typeChanged)
        {
            _typeChanged = typeChanged;
        }

        public void SetType(GeneratedElementTypeEnum newType)
        {
            _type = newType;
        }

        GeneratedElementTypeEnum IGeneratedElement.GetType()
        {
            return _type;
        }
    }
}
