using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace Ataw.Framework.Core
{
    public class ToDoItem
    {
        public string CategoryName { get; set; }

        public List<MessageModel> ItemList { get; set; }

        public List<ToDoItem> SubItemList { get; set; }

    }
}