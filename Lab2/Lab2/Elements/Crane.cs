using Lab3.GeneratingElements.Elements;

namespace Lab3.Elements
{
    public class Crane
    {
        public int id;
        public bool isServing;
        public double timeNext;
        public double timeStart;
        public IGeneratedElement shipPartOnServing1;
        public IGeneratedElement shipPartOnServing2;

        public Crane(int id)
        {
            this.id = id;
            isServing = false;
            timeNext = double.MaxValue;
        }
    }
}
