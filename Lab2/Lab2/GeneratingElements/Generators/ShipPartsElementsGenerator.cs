using Lab3.GeneratingElements.Elements;
namespace Lab3.GeneratingElements.Generators
{
    public class ShipPartsElementsGenerator : IElementsGenerator
    {
        private static int currentShipID = 0;
        public ShipPartsElementsGenerator()
        {
        }
        public (IGeneratedElement part1, IGeneratedElement part2) GenerateElement(double timeCurrent)
        {
            IGeneratedElement shipPart1 = new ShipPartGeneratedItem(currentShipID, timeCurrent);
            IGeneratedElement shipPart2 = new ShipPartGeneratedItem(currentShipID, timeCurrent);
            ShipPartsElementsGenerator.currentShipID++;
            return (shipPart1, shipPart2);
        }
    }
}
