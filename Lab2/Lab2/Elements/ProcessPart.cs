using Lab3.GeneratingElements.Elements;

namespace Lab3.Elements
{
    public class ProcessPart
    {
        public int id;
        public bool isServing;
        public double timeNext;
        public IGeneratedElement elementOnServing;

        public ProcessPart(int id)
        {
            this.id = id;
            isServing = false;
            timeNext = double.MaxValue;
        }
    }
}
