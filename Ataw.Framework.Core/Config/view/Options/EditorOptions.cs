using System.Dynamic;
using System.Collections.Generic;

namespace Ataw.Framework.Core
{
    public class EditorOptions : BaseOptions
    {
        public string FieldName { get; set; }

        public bool IsAll { get; set; }

        public bool IsHaveElementPath { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
    }
}
