
using System;
namespace Ataw.Framework.Core
{
    /// <summary>
    /// json 返回数据格式
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JsResponseResult<T>
    {
        public readonly string AKJSRES = "AKJS";
        public JsActionType ActionType { get; set; }
        public string Content { get; set; }
        public T Obj { get; set; }

        public string BeginTime { get; set; }
        public string EndTimer { get; set; }
        
        // public 

        public string ToJSON()
        {
            return AtawAppContext.Current.FastJson.ToJSON(this);
        }

        public override string ToString()
        {
            return ToJSON();
        }

    }
}
