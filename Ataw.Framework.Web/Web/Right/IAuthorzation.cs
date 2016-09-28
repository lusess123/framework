
namespace Ataw.Framework.Web
{
    public interface IAuthorzation
    {
        BaseLogonUserInfo GetLogonUserInfo(string logonId);

        string GetLogonId();
    }
}
