
namespace Ataw.Framework.Core
{
    public interface IWorkflowDefBuilder
    {
        //1成功，0失败
        int CopyWorkflowDef(string fControlUnitID);
    }
}
