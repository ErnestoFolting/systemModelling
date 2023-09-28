using Lab2.DistributionHelpers;
using Lab2.Elements;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            double meanDelay = 5; 
            IDelayProvider constDelayProvider = new ExponentialDelayProvider(meanDelay);
            IDelayProvider constDelayProvider2 = new ExponentialDelayProvider(meanDelay);

            CreateElement createElement = new CreateElement(constDelayProvider);
            ProcessElement processElement = new ProcessElement(constDelayProvider2);

            createElement.nextElement = processElement;

            processElement.maxQueueSize = 5;  // max queue size

            createElement.elementName = "CREATOR";
            processElement.elementName = "PROCESSOR";


            List<Element> elements = new List<Element>() { createElement, processElement };
            Model model = new Model(elements);

            model.Simulation(10);
        }
    }
}