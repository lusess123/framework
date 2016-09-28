
namespace Ataw.Framework.Core
{
    [CodePlug("Editor", BaseClass = typeof(AtawOptionCreator),
        CreateDate = "2012-11-16", Author = "sj", Description = "Editor控件参数创建插件")]
    public class AtawEditorOptionCreator : AtawDecodeOptionCreator
    {
        private EditorOptions fEditorOptions;

        public AtawEditorOptionCreator()
        {
            fEditorOptions = new EditorOptions();
            BaseOptions = fEditorOptions;
        }

        public override BaseOptions Create()
        {
            fEditorOptions.FieldName = this.Config.Name;
            fEditorOptions.IsAll = this.Config.Editor.IsAll;
            fEditorOptions.Width = this.Config.Editor.Width;
            fEditorOptions.Height = this.Config.Editor.Height;
            fEditorOptions.IsHaveElementPath = this.Config.Editor.IsHaveElementPath;
            return base.Create();
        }
    }
}
