﻿using Lab3.Enums;

namespace Lab3.GeneratingElements.Elements
{
    public interface IGeneratedElement
    {
        public int GetPriority();
        public GeneratedElementTypeEnum GetType();
        public void SetType(GeneratedElementTypeEnum newType);
        public void SetGenerationTime(double generationTime);
        public double GetTimeDifference(double finishTime);
        public void SetTypeChanged(bool typeChanged);
        public bool GetTypeChanged();
    }
}
