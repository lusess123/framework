namespace Ataw.Framework.Core
{
    public interface IParamExpression
    {
        string Execute(string param, object input);
    }
}