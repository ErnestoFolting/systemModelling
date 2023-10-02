namespace Lab2.Elements
{
    public struct ProcessPart
    {
        int id;
        bool isServing;

        public ProcessPart(int id)
        {
            this.id = id;
            isServing = false;
        }
    }
}
