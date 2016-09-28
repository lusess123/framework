using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ataw.Framework.Core
{
    public interface IMessage
    {
        void InsertMessage(MessageModel bean);

        void UpdateMessageStatus(string sourceID);

        IEnumerable<ToDoItem> GetToDoWork();
    }
}