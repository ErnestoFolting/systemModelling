using Lab3.DistributionHelpers;
using Lab3.Elements;
using Lab3.Enums;
using Lab3.GeneratingElements.Generators;
using Lab3.NextElementChoosingRules;
using Lab3.RuleNextElementChoosing;

namespace Lab3.Models
{
    public class HospitalModel : IModel
    {

        public List<Element> elements;

        public ProcessElement _labReception;

        public HospitalModel()
        {
            IDelayProvider DelayGenerationPatients = new ExponentialDelayProvider(15);
            IDelayProvider DelayGoingToWard = new EqualDelayProvider(3,8);
            IDelayProvider DelayGoingToOrFromLab = new EqualDelayProvider(2,5);
            IDelayProvider DelayServingOnLabReception = new ErlangDelayProvider(4.5,3);
            IDelayProvider DelayMakingAnalysis = new ErlangDelayProvider(4,2);
            IDelayProvider DelayDutyDoctors = new DutyDoctorsExponentialDelayProvider();

            IElementsGenerator elementGenerator = new PatientElementsGenerator();

            IRuleNextElementChoosing ruleByPriorityOrQueueSize = new RuleByPriorityOrQueueSize();
            IRuleNextElementChoosing ruleAfterDutyDoctors = new RuleAfterDutyDoctors();
            IRuleNextElementChoosing ruleAfterLabAnalysis = new RuleAfterLabAnalysis();

            CreateElement createElement = new CreateElement(DelayGenerationPatients, elementGenerator, ruleByPriorityOrQueueSize);
            ProcessElement dutyDoctors = new ProcessElement(DelayDutyDoctors, 2, ruleAfterDutyDoctors);
            ProcessElement goingToWard = new ProcessElement(DelayGoingToWard, 3, ruleByPriorityOrQueueSize);
            ProcessElement goingToLab = new ProcessElement(DelayGoingToOrFromLab, 100, ruleByPriorityOrQueueSize);
            ProcessElement labReception = new ProcessElement(DelayServingOnLabReception, 1, ruleByPriorityOrQueueSize);
            ProcessElement labAnalysis = new ProcessElement(DelayMakingAnalysis, 2, ruleAfterLabAnalysis);
            ProcessElement goingToDutyDoctors = new ProcessElement(DelayGoingToOrFromLab, 100, ruleByPriorityOrQueueSize);

            createElement.AddNextElement(dutyDoctors, 1);
            dutyDoctors.AddNextElement(goingToWard, 1);
            dutyDoctors.AddNextElement(goingToLab, 1);
            goingToLab.AddNextElement(labReception, 1);
            labReception.AddNextElement(labAnalysis, 1);
            labAnalysis.AddNextElement(goingToDutyDoctors, 1);
            goingToDutyDoctors.AddNextElement(dutyDoctors, 1);

            dutyDoctors.maxQueueSize = int.MaxValue;  // max queue size
            goingToWard.maxQueueSize = int.MaxValue;  // max queue size
            goingToLab.maxQueueSize = 0;  // max queue size
            labReception.maxQueueSize = int.MaxValue;  // max queue size
            labAnalysis.maxQueueSize = int.MaxValue;  // max queue size
            goingToDutyDoctors.maxQueueSize = 0;  // max queue size

            createElement.elementName = "CREATOR";
            dutyDoctors.elementName = "DUTY_DOCTORS";
            goingToWard.elementName = "GOING_TO_WARD";
            goingToLab.elementName = "GOING_TO_LAB";
            labReception.elementName = "LAB_RECEPTION";
            labAnalysis.elementName = "LAB_ANALYSIS";
            goingToDutyDoctors.elementName = "GOING_TO_DUTY_DOCTORS";

            this._labReception = labReception;

            elements = new List<Element>() { createElement, dutyDoctors, goingToWard, goingToLab, labReception, labAnalysis, goingToDutyDoctors };
        }
        public void StartSimulation()
        {
            Model model = new Model(elements);

            model.Simulation(100000, ActionPerIteration);

            OutputStats();
        }

        private void ActionPerIteration(double delta)
        {
        }

        private void OutputStats()
        {
            Console.WriteLine(CreateElement.CountDict[GeneratedElementTypeEnum.Type1]);
            Console.WriteLine(CreateElement.CountDict[GeneratedElementTypeEnum.Type2]);
            Console.WriteLine(CreateElement.CountDict[GeneratedElementTypeEnum.Type3]);


            Console.WriteLine("Type 1: " + ProcessElement.timeStats[GeneratedElementTypeEnum.Type1] / CreateElement.CountDict[GeneratedElementTypeEnum.Type1]);
            Console.WriteLine("Type 2: " + ProcessElement.timeStats[GeneratedElementTypeEnum.Type2] / CreateElement.CountDict[GeneratedElementTypeEnum.Type2]);
            Console.WriteLine("Type 3: " + ProcessElement.timeStats[GeneratedElementTypeEnum.Type3] / CreateElement.CountDict[GeneratedElementTypeEnum.Type3]);

            Console.WriteLine("Avg interval between arrivings in lab: " + this._labReception.timeCurrent / (this._labReception.exitedElements + this._labReception.queue.Count));
        }
    }
}
 