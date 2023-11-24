using Lab3.GeneratingElements.Elements;

namespace Lab3.GeneratingElements.Generators
{
    public interface IElementsGenerator
    {
        public (IGeneratedElement part1,IGeneratedElement part2) GenerateElement();
    }
}
