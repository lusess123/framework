using System.Dynamic;
using System.Collections.Generic;

namespace Ataw.Framework.Core
{
    public class ControlLegal
    {
        public LegalKind Kind { get; set; }

        public string CustomLegalFun { get; set; }

        public string Reg { get; set; }

        public string ErrMsg { get; set; }

        /// <summary>
        /// 验证表达式，为空表示验证通过，传入的参数为同一行的控件值
        /// </summary>
        public string LegalExpression { get; set; }

    }
}
