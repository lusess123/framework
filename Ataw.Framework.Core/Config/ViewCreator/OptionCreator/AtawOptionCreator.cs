using System.Dynamic;
using System.Collections.Generic;

namespace Ataw.Framework.Core
{
    public abstract class AtawOptionCreator
    {
        public AtawPageConfigView PageView { get; set; }

        public IListDataTable FormData { get; set; }

        public AtawFormConfigView FormView { get; set; }

        public ColumnConfig Config { get; set; }

        public PageStyle PageStyle { get; set; }

        public abstract BaseOptions Create();

        public void Initialize(AtawPageConfigView pageView, AtawFormConfigView formView, ColumnConfig columnConfig, PageStyle style)
        {
            this.PageView = pageView;
            this.FormView = formView;
            this.Config = columnConfig;
            this.PageStyle = style;
        }
    }
}
