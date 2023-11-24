namespace Lab3.GeneratingElements.Elements
{
    public class ShipPartGeneratedItem : IGeneratedElement
    {
        private int _shipID;
        public ShipPartGeneratedItem(int shipID)
        {
            _shipID = shipID;
        }

        public int GetElementID()
        {
            return _shipID;
        }
    }
}
