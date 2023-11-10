using Lab3.Enums;
using Lab3.GeneratingElements.Elements;
namespace Lab3.GeneratingElements.Generators
{
    public class DefaultElementsGenerator : IElementsGenerator
    {
        public DefaultElementsGenerator()
        {
        }
        public IGeneratedElement GenerateElement()
        { 
            IGeneratedElement element = new DefaultGeneratedElement(GeneratedElementTypeEnum.Type1);
            return element;
        }
    }
}
