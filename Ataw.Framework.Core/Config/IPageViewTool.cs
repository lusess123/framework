using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ataw.Framework.Core
{
  public interface IPageViewTool
    {
       ModuleConfig ModuleObj { get; }
       string Xml { get; }
       string Ds { get; }
       string PageStyle { get; }
       string KeyValue { get; }
       string Form { get; }

       void BeginSearchFormInterceptor(ref string ds, ref string xml, ref string form, ref  ModuleConfig moduleObj);
       void BeginModuleInterceptor(ref string ds, ref string xml, ref string pageStyle, ref string keyValue, ref  ModuleConfig moduleObj);
       void BeginModulePageInterceptor(ref string ds, ref string xml, ref string keyValue, ref  ModuleConfig moduleObj);
       void BeginModuleMergeInterceptor(ref string ds, ref string xml, ref string pageStyle, ref  ModuleConfig moduleObj);

       string EndSearchFormInterceptor(AtawPageConfigView view);
       string EndModuleInterceptor(AtawPageConfigView view);
       string EndModulePageInterceptor(AtawPageConfigView view);
       string EndModuleMergeInterceptor(int submitInt, int dataSourceInt, IEnumerable<string> insertKeys);
    }
}
