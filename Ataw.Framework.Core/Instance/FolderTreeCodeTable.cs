using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Ataw.Framework.Core
{
   public abstract class FolderTreeCodeTable:SimpleDataTreeCodeTable
    {
       private List<TreeCodeTableModel> fFileDataList;

       private string GetPathByFileName(string name)
       {
         return  Path.Combine(AtawAppContext.Current.BinPath, AtawApplicationConfig.REAL_PATH,
             name);
       }

       public FolderTreeCodeTable()
       {
           fFileDataList = new List<TreeCodeTableModel>();

           //string fpath = Path.Combine(AtawAppContext.Current.BinPath, AtawApplicationConfig.REAL_PATH,
           //    "module");
           //fFileDataList.Add(new TreeCodeTableModel() { 
           //     ParentId = "0",
           //     CODE_TEXT = FolderPath,
           //     CODE_VALUE = FolderPath,
           //        isParent = true,
           //         IsLeaf = false
           //});
           FileUtil.ConfirmPath(GetPathByFileName(FolderPath));
           SetFileDataList(GetPathByFileName(FolderPath), fFileDataList,FolderPath,true);
          // string[] xmls = Directory.GetFiles(fpath, "*.xml", SearchOption.AllDirectories);
           SimpleDataList = fFileDataList.OrderByDescending(a=>a.isParent);

       }

       private void SetFileDataList(string root, List<TreeCodeTableModel> fileDataList,string name,bool isRoot)
       {
           string[] fStrs = Directory.GetFileSystemEntries(root, "*", SearchOption.TopDirectoryOnly);
            foreach (string fstr in fStrs)
          {
              if (Directory.Exists(fstr))
              {
                 string  fstr2 = Path.GetFileName(fstr);
                  TreeCodeTableModel ctm = new TreeCodeTableModel();
                  ctm.CODE_VALUE = name + "\\" + fstr2;
                  ctm.CODE_TEXT = fstr2;
                  if (isRoot) {
                      ctm.ParentId = "0";
                  }
                  else
                  {
                      ctm.ParentId = name;
                  }
                  ctm.isParent = true;
                  ctm.IsLeaf = false;
                  fileDataList.Add(ctm);
                  //sParent = true;
                  SetFileDataList(root + "\\" + fstr2, fileDataList, name + "\\" + fstr2,false);

              }
              else
              {
                  string fstr2 = Path.GetFileName(fstr);
                  TreeCodeTableModel ctm = new TreeCodeTableModel();
                  ctm.CODE_VALUE = name + "\\" + Path.GetFileName(fstr2);
                  ctm.CODE_TEXT = this.setFileText(ctm.CODE_VALUE);

                  if (isRoot)
                  {
                      ctm.ParentId = "0";
                  }
                  else
                  {
                      ctm.ParentId = name;
                  }

                  ctm.isParent = false;
                  ctm.IsLeaf = true;
                  fileDataList.Add(ctm);
              }
          }
       }

       public abstract string FolderPath
       {
           get;
           set;
       }

       protected override IEnumerable<TreeCodeTableModel> SimpleDataList
       {
           get;
           set;
       }

       protected virtual string setFileText(string codeValue)
       {
           return codeValue;
       }


    }
}
