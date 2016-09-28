using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Ataw.Framework.Core;
using System.IO;

namespace Ataw.Framework.Web
{
   public class FileController:AtawBaseController
    {
       public ActionResult File(string config, string pathid, string fileid, string size, string ext)
       {
           FilePathResult res = null;
           try
           {
               int _intPath = pathid.Value<int>();
               string _pth = FileManagementUtil.GetFullPath(config, FilePathScheme.Physical, _intPath, fileid, ext);
               string _pyth =  new Uri(_pth).LocalPath;

               if (!size.IsEmpty())
               {
                   _pyth = FileUtil.ChangeFileNameAddSize(_pyth, size);
               }

               bool hasFile = System.IO.File.Exists(_pyth);
               if (hasFile)
               {
                   res = new FilePathResult(_pyth, "image");
               }
               else
               {
                   string _str = Path.Combine(AtawAppContext.Current.MapPath, "Content\\default.jpg");
                   res = new FilePathResult(_str, "image");
               }
           }
           catch
           {
              // AtawAppContext.Current.WebRootPath
               string _str = Path.Combine(AtawAppContext.Current.MapPath, "Content\\default.jpg");
               res = new FilePathResult(_str, "image");
           }
           return res;
       }
    }
}
