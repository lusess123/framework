﻿
namespace Ataw.Framework.Core
{
    public class AtawColumnConfigView
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Prompt { get; set; }
        public string ValPrompt { get; set; }

        public ColumnKind Kind { get; set; }

        public ControlType ControlType { get; set; }

        public int ShowType { get; set; }

        public bool Sortable { get; set; }

        public bool IsDetailLink { get; set; }

        public BaseOptions Options { get; set; }

        public string ChangeEventFun { get; set; }
        //public FileUploadOptions Upload { get; set; }

        public MarkDownConfig MarkDown { get; set; }

        public ReportConfig Report { get; set; }

        public EditorConfig Editor { get; set; }

        public string QingColumnName { get; set; }

        public TreeConfig TreeConfig { get; set; }

        public AmountConfig Amount { get; set; }

        public ChangerViewConfig Changer { get; set; }

        public string LinkFormat { get; set; }

        public string NormalStyle { get; set; }
        public string Width { get; set; }
        public string TdStyle { get; set; }
        public string ShortCutName { get; set; }

        public CustomControlConfig CustomControl { get; set; }

        public bool IsHiddenCol { get; set; }

    }
}
