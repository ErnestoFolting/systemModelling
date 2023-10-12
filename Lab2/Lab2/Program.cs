using Lab2.DistributionHelpers;
using Lab2.Elements;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            IDelayProvider ExponentialDelayProvider1 = new ExponentialDelayProvider(4);
            IDelayProvider ExponentialDelayProvider2 = new ExponentialDelayProvider(4);
            IDelayProvider ExponentialDelayProvider3 = new ExponentialDelayProvider(2);
            IDelayProvider ExponentialDelayProvider4 = new ExponentialDelayProvider(1);

            CreateElement createElement = new CreateElement(ExponentialDelayProvider1);
            ProcessElement processElement1 = new ProcessElement(ExponentialDelayProvider2, 1);
            ProcessElement processElement2 = new ProcessElement(ExponentialDelayProvider3, 2);
            ProcessElement processElement3 = new ProcessElement(ExponentialDelayProvider4, 1);

            createElement.AddNextElement(processElement1,1);

            processElement1.AddNextElement(processElement2, 0.8);
            processElement1.AddNextElement(processElement3, 0.2);

            processElement2.AddNextElement(processElement1, 0.4);
            processElement2.AddNextElement(processElement3, 0.6);


            processElement1.maxQueueSize = 15;  // max queue size
            processElement2.maxQueueSize = 5;  // max queue size
            processElement3.maxQueueSize = 5;  // max queue size

            createElement.elementName = "CREATOR";
            processElement1.elementName = "PROCESSOR1";
            processElement2.elementName = "PROCESSOR2";
            processElement3.elementName = "PROCESSOR3";


            List<Element> elements = new List<Element>() { createElement, processElement1, processElement2, processElement3 };
            Model model = new Model(elements);

            model.Simulation(1000);
        }
    }
}