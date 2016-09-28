
namespace Ataw.Framework.Core
{
    [CodePlug("DetailForm", BaseClass = typeof(AtawBaseFormViewCreator),
   CreateDate = "2012-12-13", Author = "sj", Description = "DetailForm创建插件")]
    public class AtawDetailFormViewCreator : AtawBaseFormViewCreator
    {
        public AtawDetailFormViewCreator()
        {
            PageStyle = PageStyle.Detail;
        }

        protected override AtawColumnConfigView CreateColumn(AtawFormConfigView formView, ColumnConfig column)
        {
            var col = base.CreateColumn(formView, column);
            switch (column.ControlType)
            {
                case ControlType.MultiFileUpload:
                case ControlType.SingleFileUpload:
                    col.ControlType = ControlType.FileDetail;
                    break;
                case ControlType.SingleImageUpload:
                case ControlType.MultiImageUpload:
                    col.ControlType = ControlType.AllImageShow;
                    break;
              
                case ControlType.Date:
                    col.ControlType = ControlType.DetailDate;
                    break;
                case ControlType.Editor:
                    col.ControlType = ControlType.EditorDetail;
                    break;
                case ControlType.InnerForm:
                    col.ControlType = ControlType.InnerForm;
                    break;
                case ControlType.DocumentSelector:
                    col.ControlType = ControlType.DocumentDetail;
                    break;
                case ControlType.Amount:
                    col.ControlType = ControlType.AmountDetail;
                    break;
                case ControlType.AmountDetail:
                    col.ControlType = ControlType.AmountDetail;
                    break;

                case ControlType.TwoColumns:
                    col.ControlType = ControlType.TwoColumnsDetail;
                    break;
                case ControlType.TwoColumnsDetail:
                    col.ControlType = ControlType.TwoColumnsDetail;
                    break;
                case ControlType.TextArea:
                    col.ControlType = ControlType.DetailArea;
                    break;
                case ControlType.DetailArea:
                    col.ControlType = ControlType.DetailArea;
                    break;
                default:
                    if (column.ControlType != ControlType.Hidden
                        && column.ControlType != ControlType.AllImageShow
                        && column.ControlType != ControlType.ImageDetail
                        )
                        col.ControlType = ControlType.Detail;
                    break;
            }
            return col;
        }
    }
}
