using Lab2.DistributionHelpers;
using Lab2.Elements;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            double meanDelay = 5; 
            IDelayProvider ConstantValueProvider = new ExponentialDelayProvider(meanDelay);

            CreateElement createElement = new CreateElement(ConstantValueProvider);
            ProcessElement processElement = new ProcessElement(ConstantValueProvider);
            ProcessElement processElement2 = new ProcessElement(ConstantValueProvider);

            createElement.nextElement = processElement;
            processElement.nextElement = processElement2;

            processElement.maxQueueSize = 5;  // max queue size
            processElement2.maxQueueSize = 5;  // max queue size

            createElement.elementName = "CREATOR";
            processElement.elementName = "PROCESSOR1";
            processElement2.elementName = "PROCESSOR2";


            List<Element> elements = new List<Element>() { createElement, processElement, processElement2 };
            Model model = new Model(elements);

            model.Simulation(20);
        }
    }
}