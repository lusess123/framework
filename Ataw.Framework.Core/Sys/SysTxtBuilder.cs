using System.Text;
namespace Ataw.Framework.Core
{
    public class SysTxtBuilder
    {
        private static readonly SysTxtBuilder instance = new SysTxtBuilder();
        private StringBuilder fStringBuilder;
        public static SysTxtBuilder Current
        {
            get
            {
                return instance;
            }
        }
        private SysTxtBuilder()
        {
            fStringBuilder = new StringBuilder();
        }

        public void Append(string txt)
        {
            fStringBuilder.Append(txt);
        }

        public void NewLine()
        {
            fStringBuilder.Append(System.Environment.NewLine);
        }
        public void Flush(LogType logType)
        {
            if (fStringBuilder.Length > 0)
            {
                AtawTrace.WriteFile(logType, fStringBuilder.ToString());
            }
            fStringBuilder = new StringBuilder();
        }
    }
}
