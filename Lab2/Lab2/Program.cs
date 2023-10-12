using Lab3.DistributionHelpers;
using Lab3.Elements;
using Lab3.Enums;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {

            //bank task
            IDelayProvider ExponentialDelayProvider1 = new ExponentialDelayProvider(0.5);
            IDelayProvider ExponentialDelayProvider2 = new ExponentialDelayProvider(0.3);

            CreateElement createElement = new CreateElement(ExponentialDelayProvider1);
            ProcessElement processElement1 = new ProcessElement(ExponentialDelayProvider2, 1);
            ProcessElement processElement2 = new ProcessElement(ExponentialDelayProvider2, 1);

            createElement.AddNextElement(processElement1, 1);
            createElement.AddNextElement(processElement2, 1);

            processElement1.maxQueueSize = 3;  // max queue size
            processElement2.maxQueueSize = 3;  // max queue size

            createElement.elementName = "CREATOR";
            processElement1.elementName = "PROCESSOR1";
            processElement2.elementName = "PROCESSOR2";


            List<Element> elements = new List<Element>() { createElement, processElement1, processElement2 };
            Model model = new Model(elements, NextElementChoosingRule.byChance);

            model.Simulation(1000);
        }
    }
}