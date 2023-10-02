using Lab2.DistributionHelpers;
using Lab2.Elements;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            double meanDelay = 5; 
            IDelayProvider ExponentialDelayProvider1 = new ExponentialDelayProvider(1);
            IDelayProvider ExponentialDelayProvider2 = new ExponentialDelayProvider(meanDelay);

            CreateElement createElement = new CreateElement(ExponentialDelayProvider1);
            ProcessElement processElement = new ProcessElement(ExponentialDelayProvider2, 3);
            ProcessElement processElement2 = new ProcessElement(ExponentialDelayProvider2, 1);
            ProcessElement processElement3 = new ProcessElement(ExponentialDelayProvider2, 1);

            createElement.nextElement = processElement;
            processElement.nextElement = processElement2;
            processElement2.nextElement = processElement3;

            processElement.maxQueueSize = 5;  // max queue size
            processElement2.maxQueueSize = 5;  // max queue size
            processElement3.maxQueueSize = 5;  // max queue size

            createElement.elementName = "CREATOR";
            processElement.elementName = "PROCESSOR1";
            processElement2.elementName = "PROCESSOR2";
            processElement3.elementName = "PROCESSOR3";


            List<Element> elements = new List<Element>() { createElement, processElement, processElement2, processElement3 };
            Model model = new Model(elements);

            model.Simulation(1000);
        }
    }
}