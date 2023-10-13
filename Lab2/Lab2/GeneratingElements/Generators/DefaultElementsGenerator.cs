using Lab3.Enums;
using Lab3.GeneratingElements.Elements;
using Lab3.Helpers;

namespace Lab3.GeneratingElements.Generators
{
    public class DefaultElementsGenerator : IElementsGenerator
    {
        private List<(GeneratedElementTypeEnum type, double chance)> generatingRules;
        public DefaultElementsGenerator()
        {
            generatingRules = new()
            {
                (GeneratedElementTypeEnum.Type1, 1)
            };
        }
        public IGeneratedElement GenerateElement()
        {
            GeneratedElementTypeEnum typeToCreate = WeightedRandomHelper.GetRandomGeneratedElementType(generatingRules);
            IGeneratedElement element = new DefaultGeneratedElement(typeToCreate);
            return element;
        }
    }
}
