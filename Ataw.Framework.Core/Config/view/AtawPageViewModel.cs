using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Ataw.Framework.Core
{
    public class AtawPageViewModel
    {
      
        public DataSet Data { get; set; }
        public AtawPageConfigView Config { get; set; }
    }

    public class PageViewModelCreator
    {
        public IEnumerable<DataFormConfig> DataFormConfigs { get; set; }
        public string DataSourceRegName { get; set; }
        protected AtawPageViewModel result { get; set; }
        public AtawPageViewModel Create()
        {
            result = new AtawPageViewModel();
            //数据载入完毕
            result.Data = new DataSet();
            //result.Data.Tables.Add(DataSourceRegName.PlugIn<DataTable>());
            // result.Data.Aggregate(result.Data);
            var listDataTable = DataSourceRegName.CodePlugIn<IListDataTable>();

            foreach (DataFormConfig form in DataFormConfigs)
            {
                AtawFormConfigView formView = new AtawFormConfigView();
                formView.FormType = FormType.Normal;
                formView.ShowKind = ShowKind.Tile;
                formView.TableName = form.TableName;
                formView.Title = form.Title;

                formView.Columns = new List<AtawColumnConfigView>();
                foreach (var column in form.Columns)
                {
                    AtawColumnConfigView to = new AtawColumnConfigView();
                    to.ControlType = column.ControlType;
                    to.DisplayName = column.DisplayName;
					to.Prompt = column.Prompt;
					to.ValPrompt = column.ValPrompt;
                    to.Name = column.Name;

                    string _controlRegname = column.ControlType.ToString();
                    // to.Options 
                    var _optionCreator = _controlRegname.CodePlugIn<AtawOptionCreator>();
                    //初始化
                    _optionCreator.Config = column;
                    // _optionCreator.FormData = result.Data[formView.TableName];
                    _optionCreator.FormView = formView;
                    //方法调用
                    to.Options = _optionCreator.Create();

                }


            }
            return result;

        }


    }

    // public class 
}
