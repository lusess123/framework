using Ataw.Framework.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ataw.FrameworkExtend.Core
{
   public static class AtawPageConfigViewExtend
    {
       public static void createFillDataSet(this AtawPageConfigView _this , string formName)
       {
           AtawFormConfigView _form = _this.Forms[formName];
           DataTable dt = _this.Data.Tables[_form.TableName];
           List<string> tmpComluns = new List<string>();
           foreach (DataRow row in dt.Rows)
           {
               _form.Columns.ForEach(col =>
               {
                   //-----------
                   string _val = row[col.Name].ToString();
                   if (!col.Options.RegName.IsEmpty())
                   {
                       //------------
                       string _indexName = col.Name + "_CODEINDEX";
                       if (row.Table.Columns.Contains(_indexName))
                       {
                           if (row[_indexName] is int[])
                           {
                               int[] _indexs = row[_indexName].Value<int[]>();
                               DataTable _codetable = _this.Data.Tables[col.Options.RegName];
                               List<string> _texts = new List<string>();
                               foreach (int index in _indexs)
                               {
                                   if (index >= 0)
                                   {
                                       DataRow _row = _codetable.Rows[index];

                                       string _text = _row["CODE_TEXT"].ToString();
                                       Regex reg = new Regex(@"<[^<>]+>");
                                       // string str = "_text";
                                       // ;
                                       bool isMath = true;
                                       while (isMath)
                                       {
                                           var math = reg.Match(_text);
                                           isMath = math.Success;
                                           if (isMath)
                                           {
                                               _text = _text.Replace(math.Value, "");
                                           }
                                       }

                                       _texts.Add(_text);
                                   }
                               }
                               //if()
                               if (row.Table.Columns[col.Name].DataType == typeof(string))
                               {
                                   row[col.Name] = _texts.JoinString(",");
                               }
                               else
                               {
                                   if (!row.Table.Columns.Contains(col.Name + "_temp"))
                                   {
                                       row.Table.Columns.Add(col.Name + "_temp", typeof(string));
                                       tmpComluns.Add(col.Name + "_temp");
                                   }
                                   row[col.Name + "_temp"] = _texts.JoinString(",");
                                   
                                  
                                  
                               }


                           }
                           else
                           {
                               int _index = row[_indexName].Value<int>();
                               DataTable _codetable = _this.Data.Tables[col.Options.RegName];
                               DataRow _row = _codetable.Rows[_index];
                               string _text = _row["CODE_TEXT"].ToString();
                               //-------------
                               row[col.Name] = _text;
                           }
                       }


                   }

               });
           }

           foreach (string col in tmpComluns) {
               string _c = col;
               string _col = col.Remove(col.LastIndexOf("_temp"));
               dt.Columns.Remove(_col);
               dt.Columns[_c].ColumnName = _col;
           }

           //----------
           //   return this.Data;
       }
    }
}
