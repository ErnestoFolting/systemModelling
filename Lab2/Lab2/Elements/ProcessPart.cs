namespace Lab2.Elements
{
    public class ProcessPart
    {
        public int id;
        public bool isServing;
        public double timeNext;

        public ProcessPart(int id)
        {
            this.id = id;
            isServing = false;
            timeNext = double.MaxValue;
        }
    }
}
