using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ataw.Framework.Core
{
    [CodePlug("InsertForm", BaseClass = typeof(AtawBaseFormViewCreator),
    CreateDate = "2012-12-13", Author = "sj", Description = "InserForm创建插件")]
    public class AtawInsertFormViewCreator : AtawBaseFormViewCreator
    {
        public AtawInsertFormViewCreator()
        {
            PageStyle = PageStyle.Insert;
        }

        public override IEnumerable<AtawFormConfigView> Create()
        {
            var formViews = base.Create();
            var formView = formViews.First();
            if (ModuleConfig.Relations != null) //子表支持批量新增
            {
                var relation = this.ModuleConfig.Relations.FirstOrDefault(a => a.DetailForm == FormConfig.Name);
                if (relation != null)
                {
                    if (relation.MasterField != relation.DetailField)
                    {
                        formView.HasBatchInsert = true;
                    }
                    var insertFormView = CreateBatchInsertForm(formView);
                    var viewList = formViews.ToList();
                    viewList.Add(insertFormView);
                    formViews = viewList;
                }
            }
            return formViews;
        }

        private AtawFormConfigView CreateBatchInsertForm(AtawFormConfigView formView)
        {
            AtawFormConfigView insertFormView = new AtawFormConfigView();
            insertFormView.FormType = FormType.Normal;
            insertFormView.ShowKind = ShowKind.Tile;
            insertFormView.AfterInitFunName = formView.AfterInitFunName;
            insertFormView.TableName = formView.TableName;
            insertFormView.PrimaryKey = formView.PrimaryKey;
            insertFormView.Title = formView.Title;
            insertFormView.Name = formView.Name + "_INSERT";
            insertFormView.Columns = formView.Columns;
            return insertFormView;
        }
    }
}
