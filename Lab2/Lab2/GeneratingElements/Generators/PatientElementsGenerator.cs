using Lab3.Enums;
using Lab3.GeneratingElements.Elements;
using Lab3.Helpers;

namespace Lab3.GeneratingElements.Generators
{
    public class PatientElementsGenerator : IElementsGenerator
    {
        private List<(GeneratedElementTypeEnum type, double chance)> generatingRules;
        public PatientElementsGenerator()
        {
            generatingRules = new()
            {
                (GeneratedElementTypeEnum.Type1, 0.5),
                (GeneratedElementTypeEnum.Type2, 0.1),
                (GeneratedElementTypeEnum.Type3, 0.4)
            };
        }
        public IGeneratedElement GenerateElement()
        {
            GeneratedElementTypeEnum typeToCreate = WeightedRandomHelper.GetRandomGeneratedElementType(generatingRules);
            IGeneratedElement element = new PatientGeneratedElement(typeToCreate);
            return element;
        }
    }
}
