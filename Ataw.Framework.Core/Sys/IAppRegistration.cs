
using System;
namespace Ataw.Framework.Core
{
    public interface IAppRegistration
    {
        void Initialization();

        void SetAssamblyClassType(Type type);
    }
}
