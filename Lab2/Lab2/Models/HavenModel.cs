using Lab3.Elements;
using Lab3.GeneratingElements.Generators;
using Lab3.Helpers.DistributionHelpers;
using Lab3.Helpers.Loggers;
using Lab3.NextElementChoosingRules;
using Lab3.RuleNextElementChoosing;

namespace Lab3.Models
{
    public class HavenModel : IModel
    {
        public List<Element> elements;
        private ILogger _logger;
        public HavenModel(ILogger logger)
        {
            _logger = logger;

            IDelayProvider shipArrivingDelay = new ExponentialDelayProvider(1.25) ;
            IDelayProvider processDelay = new EqualDelayProvider(0.5,1.5);
            IElementsGenerator generator = new ShipPartsElementsGenerator();
            IRuleNextElementChoosing ruleNextElementChoosing = new RuleByChance();

            CreateElement shipsCreation = new(shipArrivingDelay, generator, ruleNextElementChoosing, _logger);
            ProcessElement cargoCrane = new(processDelay, 2, ruleNextElementChoosing, _logger);

            shipsCreation.AddNextElement(cargoCrane, 1);

            cargoCrane.maxQueueSize = int.MaxValue;

            elements = new() { shipsCreation, cargoCrane };

            shipsCreation.elementName = "CREATE";
            cargoCrane.elementName = "PROCESS";
        }
        public void StartSimulation()
        {
            Model model = new(elements, _logger);
            model.Simulation(500, null);
        }
    }
}
