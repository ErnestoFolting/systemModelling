using Lab3.DistributionHelpers;
using Lab3.Elements;
using Lab3.GeneratingElements.Generators;
using Lab3.NextElementChoosingRules;
using Lab3.RuleNextElementChoosing;

namespace Lab3.Models
{
    public class HavenModel : IModel
    {
        public List<Element> elements;
        public HavenModel()
        {
            IDelayProvider shipArrivingDelay = new ExponentialDelayProvider(0.75);
            IDelayProvider processDelay = new EqualDelayProvider(0.5,1.5);
            IElementsGenerator generator = new ShipPartsElementsGenerator();
            IRuleNextElementChoosing ruleNextElementChoosing = new RuleByChance();

            CreateElement shipsCreation = new(shipArrivingDelay, generator, ruleNextElementChoosing);
            ProcessElement cargoCrane = new(processDelay, 2, ruleNextElementChoosing);

            shipsCreation.AddNextElement(cargoCrane, 1);

            cargoCrane.maxQueueSize = int.MaxValue;

            elements = new() { shipsCreation, cargoCrane };

            shipsCreation.elementName = "CREATE";
            cargoCrane.elementName = "PROCESS";
        }
        public void StartSimulation()
        {
            Model model = new(elements);
            model.Simulation(10, null);
        }
    }
}
