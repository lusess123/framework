using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Threading;

namespace Ataw.Framework.Core
{
    internal class FileLogger
    {
        public static readonly FileLogger Instance = new FileLogger();
        private FileLogger()
        {
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);
            AppDomain.CurrentDomain.DomainUnload += new EventHandler(CurrentDomain_ProcessExit);

            _worker = new Thread(new ThreadStart(Writer));
            _worker.IsBackground = true;
            _worker.Start();
        }

        private Thread _worker;
        private bool _working = true;
        private Queue _que = new Queue();
        private StreamWriter _output;
        private string _filename;
        private int _sizeLimit = 0;
        private long _lastSize = 0;
        private DateTime _lastFileDate;
        private bool _showMethodName = false;
        private string _FilePath = "";

        public bool ShowMethodNames
        {
            get { return _showMethodName; }
        }

        public void Init(string filename, int sizelimitKB, bool showmethodnames)
        {
            _showMethodName = showmethodnames;
            _sizeLimit = sizelimitKB;
            _filename = filename;
            // handle folder names as well -> create dir etc.
            _FilePath = Path.GetDirectoryName(filename);
            if (_FilePath != "")
            {
                _FilePath = Directory.CreateDirectory(_FilePath).FullName;
                if (_FilePath.EndsWith("\\") == false)
                    _FilePath += "\\";
            }
            _output = new StreamWriter(filename, true);
            FileInfo fi = new FileInfo(filename);
            _lastSize = fi.Length;
            _lastFileDate = fi.LastWriteTime;
        }

        private void shutdown()
        {
            _working = false;
            _worker.Abort();
        }

        void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            shutdown();
        }

        private void Writer()
        {
            while (_working)
            {
                while (_que.Count > 0)
                {
                    object o = _que.Dequeue();
                    if (_output != null && o != null)
                    {
                        if (_sizeLimit > 0)
                        {
                            // implement size limited logs
                            // implement rolling logs
                            #region [  rolling size limit ]
                            _lastSize += ("" + o).Length;
                            if (_lastSize > _sizeLimit * 1000)
                            {
                                _output.Flush();
                                _output.Close();
                                int count = 1;
                                while (File.Exists(_FilePath + Path.GetFileNameWithoutExtension(_filename) + "." + count.ToString("0000")))
                                    count++;

                                File.Move(_filename,
                                    _FilePath +
                                    Path.GetFileNameWithoutExtension(_filename) +
                                    "." + count.ToString("0000"));
                                _output = new StreamWriter(_filename, true);
                                _lastSize = 0;
                            }
                            #endregion
                        }
                        if (DateTime.Now.Subtract(_lastFileDate).Days > 0)
                        {
                            // implement date logs
                            #region [  rolling dates  ]
                            _output.Flush();
                            _output.Close();
                            int count = 1;
                            while (File.Exists(_FilePath + Path.GetFileNameWithoutExtension(_filename) + "." + count.ToString("0000")))
                            {
                                File.Move(_FilePath + Path.GetFileNameWithoutExtension(_filename) + "." + count.ToString("0000"),
                                   _FilePath +
                                   Path.GetFileNameWithoutExtension(_filename) +
                                   "." + count.ToString("0000") +
                                   "." + _lastFileDate.ToString("yyyy-MM-dd"));
                                count++;
                            }
                            File.Move(_filename,
                               _FilePath +
                               Path.GetFileNameWithoutExtension(_filename) +
                               "." + count.ToString("0000") +
                               "." + _lastFileDate.ToString("yyyy-MM-dd"));

                            _output = new StreamWriter(_filename, true);
                            _lastFileDate = DateTime.Now;
                            _lastSize = 0;
                            #endregion
                        }
                        _output.Write(o);
                    }
                }
                if (_output != null)
                    _output.Flush();
                Thread.Sleep(500);
            }
            _output.Flush();
            _output.Close();
        }

        private string FormatLog(string log, string type, string meth, string msg, object[] objs)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(
                "" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") +
                "|" + log +
                "|" + Thread.CurrentThread.ManagedThreadId +
                "|" + type +
                "|" + meth +
                "| " + msg);

            foreach (object o in objs)
                sb.AppendLine("" + o);

            return sb.ToString();
        }

        public void Log(string logtype, string type, string meth, string msg, params object[] objs)
        {
            _que.Enqueue(FormatLog(logtype, type, meth, msg, objs));
        }
    }
}
