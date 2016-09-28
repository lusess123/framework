
namespace Ataw.Framework.Core
{
    /// <summary>
    /// 响应返回值
    /// </summary>
    public class ResponseResult
    {
        /// <summary>
        /// ，>=0 表示成功<0 表示有异常
        /// </summary>
        public int Result
        {
            get;
            set;
        }
        /// <summary>
        /// 异常信息
        /// </summary>
        public string Error
        {
            get;
            set;
        }
        /// <summary>
        /// 附加返回值
        /// </summary>
        public object Content
        {
            get;
            set;
        }
    }
}
