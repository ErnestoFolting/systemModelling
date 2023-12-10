namespace Lab3.GeneratingElements.Elements
{
    public class ShipPartGeneratedItem : IGeneratedElement
    {
        private int _shipID;
        private double _arrivingTime;
        public ShipPartGeneratedItem(int shipID, double timeCurrent)
        {
            _shipID = shipID;
            _arrivingTime = timeCurrent;
        }

        public int GetElementID()
        {
            return _shipID;
        }

        public double GetTimeOfServing(double timeCurrent)
        {
            return timeCurrent - _arrivingTime;
        }
    }
}
