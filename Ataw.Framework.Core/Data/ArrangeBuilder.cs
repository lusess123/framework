
using System.Collections.Generic;
namespace Ataw.Framework.Core
{
    public class ArrangeBuilder
    {
        private List<string> fArrange;

        protected int fLayerLevel;
        protected int LastNodeIndex { get; set; }

        protected string BeforeArrange { get; set; }

        public ArrangeBuilder()
        {
            fArrange = new List<string>();
        }

        private string SetTextIndex()
        {
            AtawDebug.Assert(LastNodeIndex >= 0 && LastNodeIndex < 1000, "同一个级的树节点不能超过1000个", this);
            string str = string.Format(ObjectUtil.SysCulture, "{0:000}", LastNodeIndex);
            return str;
        }

        private string GetTextJoin(string str)
        {
            if (BeforeArrange.IsEmpty())
            {
                return str;
            }
            else
                return string.Format(ObjectUtil.SysCulture, "{0}_{1}", BeforeArrange, str);
        }

        public IEnumerable<string> Arrange
        {
            get
            {
                return fArrange;
            }
        }

        public int LayerLevel
        {
            get
            {
                return fLayerLevel;
            }
        }

        public void AddNode()
        {
            LastNodeIndex++;
        }

        public void AddLevelNode()
        {
            fLayerLevel++;
            string str = SetTextIndex();
            BeforeArrange = GetTextJoin(str);
            LastNodeIndex = 0;
        }

        // public 
        public string GetText()
        {
            return GetTextInternal();
            //return BeforeArrange;
        }

        private string GetTextInternal()
        {
            string str = SetTextIndex();
            return GetTextJoin(str);
        }

        public ArrangeBuilder Copy()
        {
            return new ArrangeBuilder()
            {
                BeforeArrange = this.BeforeArrange,
                LastNodeIndex = this.LastNodeIndex,
                fLayerLevel = this.fLayerLevel,
                fArrange = new List<string>()
            };
        }


    }
}
