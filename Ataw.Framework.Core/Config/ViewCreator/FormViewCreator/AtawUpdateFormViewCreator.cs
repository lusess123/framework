using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ataw.Framework.Core
{
    [CodePlug("UpdateForm", BaseClass = typeof(AtawBaseFormViewCreator),
    CreateDate = "2012-12-13", Author = "sj", Description = "UpdateForm创建插件")]
    public class AtawUpdateFormViewCreator : AtawBaseFormViewCreator
    {
        public AtawUpdateFormViewCreator()
        {
            PageStyle = PageStyle.Update;
        }

        public override IEnumerable<AtawFormConfigView> Create()
        {
            var formViews = base.Create();
            var formView = formViews.First();
            bool hasBatchInsert = false;
            if (FormConfig.HasBatchInsert)
            {
                formView.HasBatchInsert = true;
                hasBatchInsert = true;
            }
            //当ModulePage的Form的Action为Insert的时候，是以Update的批量新增来创建Form的，这里需要把Action还原成Insert
            if (ModuleConfig.Mode == ModuleMode.None && FormConfig.Action == PageStyle.Insert)
            {
                formView.Action = PageStyle.Insert;
                hasBatchInsert = true;
            }
            if (hasBatchInsert)
            {
                var insertFormView = CreateBatchInsertForm(formView);
                var viewList = formViews.ToList();
                viewList.Add(insertFormView);
                formViews = viewList;
            }
            PageStyle = PageStyle.Update;
            return formViews;
        }

        private AtawFormConfigView CreateBatchInsertForm(AtawFormConfigView formView)
        {
            PageStyle = PageStyle.Insert;
            AtawFormConfigView insertFormView = new AtawFormConfigView();
            insertFormView.FormType = FormType.Normal;
            insertFormView.ShowKind = ShowKind.Tile;
            insertFormView.TableName = formView.TableName;
            insertFormView.PrimaryKey = formView.PrimaryKey;
            insertFormView.Title = formView.Title.Substring(0, formView.Title.Length - 2) + "新增";
            insertFormView.Name = formView.Name + "_INSERT";
            insertFormView.Columns = new List<AtawColumnConfigView>();
            //Grid的批量新增，新增字段要与修改字段保存一致, 其他类型的表单可以不一致---------------------------重要的约定
            if (FormConfig.FormType == FormType.Grid)
            {
                formView.Columns.ForEach(a =>
                {

                    var column = DataFormConfig.Columns.FirstOrDefault(b => b.Name == a.Name);
                    //Grid支持批量新增时，若字段配置新增页面不显示，那新增的时候该字段的控件类型应为Detail
                    if ((column.ShowPage & PageStyle.Insert) != PageStyle.Insert)
                    {
                        var col = CreateColumn(insertFormView, column);
                        col.ControlType = ControlType.Detail;
                        insertFormView.Columns.Add(col);
                    }
                    else
                        insertFormView.Columns.Add(CreateColumn(insertFormView, column));
                });
            }
            else if (FormConfig.FormType == FormType.Normal || FormConfig.FormType == FormType.Album)
            {
                DataFormConfig.Columns.ForEach(a =>
                {
                    if ((a.ShowPage & PageStyle.Insert) == PageStyle.Insert)
                    {
                        insertFormView.Columns.Add(CreateColumn(insertFormView, a));
                    }
                });
            }
            return insertFormView;
        }
    }
}
