using System.Dynamic;
using System.Collections.Generic;

namespace Ataw.Framework.Core
{
    public class MultiImageUploadOptions : SingleImageUploadOptions
    {
        /// <summary>
        /// 最小上传数
        /// </summary>
        public int MinUploadCount { get; set; }
    }
}
