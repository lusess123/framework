using System.Collections.Generic;
using System.Linq;

namespace Ataw.Framework.Core
{
    [CodePlug("UpdatePageView", BaseClass = typeof(AtawBasePageViewCreator),
       CreateDate = "2012-11-23", Author = "sj", Description = "UpdatePageView创建插件")]
    public class AtawUpdatePageViewCreator : AtawBasePageViewCreator
    {
        private AtawUpdatePageConfigView fUpdatePageConfigView;

        public AtawUpdatePageViewCreator()
        {
            fUpdatePageConfigView = new AtawUpdatePageConfigView();
            BasePageView = fUpdatePageConfigView;
            PageStyle = PageStyle.Update;
        }
        public override void SetReturnUrl()
        {
            base.SetReturnUrl();
            if (!ModuleConfig.InsertReturnUrl.IsEmpty())
            {
                BasePageView.ReturnUrl = ModuleConfig.UpdateReturnUrl;
            }
        }
        //public override AtawPageConfigView Create()
        //{
        //    var pageView = base.Create();
        //    var list = this.FormInfoList.ToList().Where(
        //    a => a.FormConfig.HasBatchInsert && (a.FormConfig.FormType == FormType.Grid || a.FormConfig.FormType == FormType.Normal));
        //    list.ToList().ForEach(a =>
        //        {
        //            var insertForm = CreateBatchInsertForm(a);
        //            pageView.Forms.Add(insertForm.Name, insertForm);
        //        });
        //    PageStyle = PageStyle.Update;
        //    return pageView;
        //}

        //private AtawFormConfigView CreateBatchInsertForm(FormConfigInfo formInfo)
        //{
        //    PageStyle = PageStyle.Insert;
        //    AtawFormConfigView formView = new AtawFormConfigView();
        //    formView.FormType = FormType.Normal;
        //    formView.ShowKind = ShowKind.Tile;
        //    formView.TableName = formInfo.FormView.TableName;
        //    formView.PrimaryKey = formInfo.FormView.PrimaryKey;
        //    formView.Title = formInfo.FormView.Title.Substring(0, formInfo.FormView.Title.Length - 2) + "新增";
        //    formView.Name = formInfo.FormView.Name + "_INSERT";
        //    formView.Columns = new List<AtawColumnConfigView>();
        //    //Grid的批量新增，新增字段要与修改字段保存一致
        //    if (formInfo.FormConfig.FormType == FormType.Grid)
        //    {
        //        formInfo.FormView.Columns.ForEach(a =>
        //            {

        //                var column = formInfo.DataForm.Columns.FirstOrDefault(b => b.Name == a.Name);
        //                //Grid支持批量新增时，若字段配置新增页面不显示，那新增的时候该字段的控件类型应为Detail
        //                if ((column.ShowPage & PageStyle.Insert) != PageStyle.Insert)
        //                {
        //                    var col = CreateColumn(formView, column);
        //                    col.ControlType = ControlType.Detail;
        //                    formView.Columns.Add(col);
        //                }
        //                else
        //                    formView.Columns.Add(a);
        //            });
        //    }
        //    else if (formInfo.FormConfig.FormType == FormType.Normal)
        //    {
        //        formInfo.DataForm.Columns.ForEach(a =>
        //            {
        //                if ((a.ShowPage & PageStyle.Insert) == PageStyle.Insert)
        //                {
        //                    formView.Columns.Add(CreateColumn(formView, a));
        //                }
        //            });
        //    }
        //    return formView;
        //}
    }
}
