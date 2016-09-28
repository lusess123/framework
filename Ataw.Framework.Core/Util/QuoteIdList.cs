using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ataw.Framework.Core
{
    public sealed class QuoteIdList : IEnumerable<string>
    {
        private readonly List<string> fIdList;

        public QuoteIdList()
        {
            fIdList = new List<string>();
        }

        #region IEnumerable<string> 成员

        public IEnumerator<string> GetEnumerator()
        {
            return fIdList.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        IEnumerator IEnumerable.GetEnumerator()
        {
            return fIdList.GetEnumerator();
        }

        #endregion

        private void FromString(string idlist)
        {
            fIdList.Clear();
            string[] arr = idlist.Split(',');

            foreach (string s in arr)
            {
                string item = s.Trim();
                AtawDebug.Assert(item.Length >= 2, string.Format(ObjectUtil.SysCulture,
                    "字符串必须是\"value\"格式的，现在值为{0}，式样不对", item), this);
                fIdList.Add(item.Substring(1, item.Length - 2));
            }
        }

        public int Count
        {
            get
            {
                return fIdList.Count;
            }
        }

        public string this[int index]
        {
            get
            {
                AtawDebug.AssertArgument(index >= 0 && index < fIdList.Count, "index",
                    string.Format(ObjectUtil.SysCulture, "index必须在0和{1}之间，现在值为{0}不在范围内",
                    index, fIdList.Count), this);
                return fIdList[index];
            }
        }

        public void Add(IEnumerable<string> values)
        {
            AtawDebug.AssertArgumentNull(values, "values", this);

            foreach (string id in values)
                Add(id);
        }

        public void Add(string value)
        {
            if (!string.IsNullOrEmpty(value))
                fIdList.Add(value);
        }

        public bool Contains(string id)
        {
            if (string.IsNullOrEmpty(id))
                return false;
            return fIdList.Contains(id);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design",
            "CA1021:AvoidOutParameters", MessageId = "0#")]
        public string ToString(out int count)
        {
            if (fIdList.Count == 0)
            {
                count = 0;
                return string.Empty;
            }
            IEnumerable<string> unique = fIdList.Distinct();
            string[] strArr = unique.ToArray();
            count = strArr.Length;
            StringBuilder builder = new StringBuilder();
            int index = 0;
            foreach (string value in strArr)
                StringUtil.JoinStringItem(builder, index++, GetQuoteId(value), ",");
            return builder.ToString();
        }

        public override string ToString()
        {
            int count;
            return ToString(out count);
        }

        public static string GetQuoteId(string id)
        {
            return string.Format(ObjectUtil.SysCulture, "\"{0}\"", id);
        }

        public static QuoteIdList LoadFromString(string ids)
        {
            QuoteIdList result = new QuoteIdList();
            if (!string.IsNullOrEmpty(ids))
                result.FromString(ids);
            return result;
        }
    }
}
