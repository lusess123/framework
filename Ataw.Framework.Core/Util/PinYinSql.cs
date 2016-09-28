using System.Collections.Generic;
using System.Text;

namespace Ataw.Framework.Core
{
    internal sealed class PinYinSql
    {
        private readonly Dictionary<char, List<char>> fPyTable;
        private readonly Dictionary<char, string> fSqlTable;
        /// <summary>
        /// Initializes a new instance of the PinYinSQL class.
        /// </summary>
        public PinYinSql()
        {
            fPyTable = new Dictionary<char, List<char>>();
            fSqlTable = new Dictionary<char, string>();
        }

        private void LoadData(string buffer)
        {
            string[] lines = buffer.Split('\n');
            foreach (string line in lines)
            {
                if (line.Length <= 1)
                    continue;
                char head = char.ToUpper(line[0], ObjectUtil.SysCulture);
                List<char> dataList;
                if (fPyTable.ContainsKey(head))
                    dataList = fPyTable[head];
                else
                {
                    dataList = new List<char>();
                    fPyTable.Add(head, dataList);
                }
                string data = line.Substring(1);
                foreach (char item in data)
                    if (item > 255)
                        dataList.Add(item);
            }
        }

        private void LoadDataFromRes()
        {
            string buffer = StrResource.Polyphone;
            LoadData(buffer);
        }

        private void ConvertSqlList()
        {
            fSqlTable.Clear();
            StringBuilder builder = new StringBuilder();
            foreach (KeyValuePair<char, List<char>> item in fPyTable)
            {
                builder.Length = 0;
                int i = 0;
                foreach (char pyChar in item.Value)
                    StringUtil.JoinStringItem(builder, i++, "'" + pyChar + "'");
                string data = builder.ToString();
                fSqlTable.Add(item.Key, data);
            }
        }

        public void Load()
        {
            LoadDataFromRes();
            ConvertSqlList();
        }

        public string this[char key]
        {
            get
            {
                if (fSqlTable.ContainsKey(key))
                    return fSqlTable[key];
                else
                    return string.Empty;
            }
        }

    }
}
