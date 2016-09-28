using System.Diagnostics;
using System.Text;

namespace Ataw.Framework.Core
{
    internal class TraceFileListener : TraceListener
    {
        public const int FILELISTENER_SIGN = 100;
        public override void Write(string message)
        {
            // throw new NotImplementedException();
        }

        public override void WriteLine(string message)
        {
            // throw new NotImplementedException();
        }

        public override void TraceData(TraceEventCache eventCache, string source,
            TraceEventType eventType, int id, params object[] data)
        {
            switch (id)
            {
                case FILELISTENER_SIGN:
                    if (data.Length == 2 || data.Length == 3)
                    {
                        string fileName = data[0].ToString();
                        string content = data[1].ToString();
                        if (data.Length == 3)
                        {
                            Encoding encoding = data[2] as Encoding;
                            if (encoding != null)
                                FileUtil.VerifySaveFile(fileName, content, encoding);
                            else
                                FileUtil.VerifySaveFile(fileName, content);
                        }
                        else
                            FileUtil.VerifySaveFile(fileName, content);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
