using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ataw.Framework.Core.Data
{
    public interface IMomery
    {
        IEnumerable<string> BeginSearch(string text);
        IEnumerable<string> Search(string text,int pageNo,int pageSize,ref int  pageCount);

        int AddText(string text,IUnitOfData unit);

        int RemoveText(string text, IUnitOfData unit);
    }
}
