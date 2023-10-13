﻿using Lab3.DistributionHelpers;
using Lab3.Elements;
using Lab3.Enums;
using Lab3.GeneratingElements.Generators;

namespace Lab3.Models
{
    public class HospitalModel : IModel
    {

        public List<Element> elements;
        public HospitalModel()
        {
            IDelayProvider DelayGenerationPatients = new ExponentialDelayProvider(15);
            IDelayProvider DelayGoingToWard = new EqualDelayProvider(3,8);
            IDelayProvider DelayGoingToOrFromLab = new EqualDelayProvider(2,5);
            IDelayProvider DelayServingOnLabReception = new ErlangDelayProvider(4.5,3);
            IDelayProvider DelayMakingAnalysis = new ErlangDelayProvider(4,2);
            IDelayProvider DelayDutyDoctors = new DutyDoctorsExponentialDelayProvider();

            IElementsGenerator elementGenerator = new PatientElementsGenerator();

            CreateElement createElement = new CreateElement(DelayGenerationPatients, elementGenerator);
            ProcessElement dutyDoctors = new ProcessElement(DelayDutyDoctors, 2);

            createElement.AddNextElement(dutyDoctors, 1);

            dutyDoctors.maxQueueSize = int.MaxValue;  // max queue size

            createElement.elementName = "CREATOR";
            dutyDoctors.elementName = "DUTY_DOCTORS";


            elements = new List<Element>() { createElement, dutyDoctors };
        }
        public void StartSimulation()
        {
            Model model = new Model(elements, NextElementChoosingRule.byPriorityOrQueueSize);

            model.Simulation(10, ActionPerIteration);
        }

        private void ActionPerIteration(double delta)
        {
            Console.WriteLine("Iteration");
        }
    }
}
 