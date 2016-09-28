using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ataw.Framework.Core
{
    public interface IMessageFilter
    {
        bool IsFiltered(MessageModel model, IUnitOfData unitOfData);
    }
}