using Lab3.Enums;

namespace Lab3.GeneratingElements.Elements
{
    public class DefaultGeneratedElement : IGeneratedElement
    {
        private int _priority;
        private GeneratedElementTypeEnum _type;
        private double _generationTime;
        private bool _typeChanged;
        public DefaultGeneratedElement(GeneratedElementTypeEnum type)
        {
            _type = type;
            _priority = 1;
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
            return finishTime - _generationTime;
        }

        public GeneratedElementTypeEnum GetType()
        {
            return _type;
        }

        public void SetGenerationTime(double generationTime)
        {
            this._generationTime = generationTime;
        }

        public void SetTypeChanged(bool typeChanged)
        {
            _typeChanged = typeChanged;
        }

        public void SetType(GeneratedElementTypeEnum newType)
        {
            _type = newType;
        }
    }
}
