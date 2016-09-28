using System.Dynamic;
using System.Collections.Generic;

namespace Ataw.Framework.Core
{
    public class MultiFileUploadOptions : SingleFileUploadOptions
    {
        public int MinUploadCount { get; set; }
    }
}
